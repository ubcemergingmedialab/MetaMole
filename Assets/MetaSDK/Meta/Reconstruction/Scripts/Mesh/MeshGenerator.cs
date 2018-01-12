using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Responsible for genetating meshes from raw vertices and triangles data
    /// </summary>
    public class MeshGenerator : IMeshGenerator
    {
        private object _lockObject = new object();

        private bool _async;
        private List<Mesh> _reconstruction;

        private Thread _thread;
        private List<Vector3[]> _verticesSaved;
        private List<int[]> _trianglesSaved;

        private int _maxTriangles;
        private Material _material;
        private Transform _parent;

        /// <summary>
        /// Material that is assigned to the meshes generated
        /// </summary>
        public Material Material
        {
            get { return _material; }
            set { _material = value; }
        }

        /// <summary>
        /// Parent of the mesh GameObjects generated
        /// </summary>
        public Transform Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Creates an instance of <see cref="MeshGenerator"/> class.
        /// </summary>
        /// <param name="async">Whether to perform an asynchronous generation process or not.</param>
        /// <param name="maxTriangles">Maximum amount of triangles contained in a mesh before splitting.</param>
        public MeshGenerator(bool async, int maxTriangles)
        {
            _maxTriangles = maxTriangles;
            _async = async;
            InitLists();
        }

        /// <summary>
        /// Split the reconstruction in multiple meshes
        /// </summary>
        /// <param name="reconstructionVertices">Vertices of the meshes</param>
        /// <param name="reconstructionTriangles">Triangles of the meshes</param>
        public void UpdateMeshes(double[] reconstructionVertices, int[] reconstructionTriangles)
        {
            // if there is no split in process
            if (_thread != null && _thread.IsAlive)
            {
                return;
            }

            // create new meshes
            while (reconstructionTriangles.Length / 3 > _maxTriangles * _reconstruction.Count)
            {
                CreateMesh(_material, _parent);
            }

            if (_async)
            {
                UpdateMeshes();

                // split meshes
                _thread = new Thread(() =>
                {
                    lock (_lockObject)
                    {
                        SplitSharedVerticesMesh(reconstructionVertices, reconstructionTriangles, _maxTriangles);
                    }
                });

                _thread.Start();
            }
            else
            {
                SplitSharedVerticesMesh(reconstructionVertices, reconstructionTriangles, _maxTriangles);
                UpdateMeshes();
            }
        }

        /// <summary>
        /// Reset the meshes data
        /// </summary>
        public void ResetMeshes()
        {
            if (_async)
            {
                lock (_lockObject)
                {
                    if (_thread.IsAlive)
                    {
                        _thread.Abort();
                    }

                    InitLists();
                }
            }
            else
            {
                InitLists();
            }
        }

        private void UpdateMeshes()
        {
            // update meshes
            for (int i = 0; i < _verticesSaved.Count; i++)
            {
                // To avoid the error: Failed setting triangles.The number of supplied triangle indices must be a multiple of 3.
                if (_trianglesSaved[i].Length % 3 != 0)
                {
                    continue;
                }
                _reconstruction[i].Clear();
                _reconstruction[i].vertices = _verticesSaved[i];
                _reconstruction[i].triangles = _trianglesSaved[i];
            }
        }

        private void InitLists()
        {
            if (_reconstruction != null)
            {
                _reconstruction.Clear();
                _verticesSaved.Clear();
                _trianglesSaved.Clear();
            }
            else
            {
                _reconstruction = new List<Mesh>();
                _verticesSaved = new List<Vector3[]>();
                _trianglesSaved = new List<int[]>();
            }
        }

        private void CreateMesh(Material material, Transform parent)
        {
            GameObject newMesh = new GameObject();
            newMesh.transform.SetParent(parent);
            newMesh.name = "reconstruction_" + (_reconstruction.Count + 1);

            MeshFilter meshFilter = newMesh.AddComponent<MeshFilter>();
            newMesh.AddComponent<MeshRenderer>().material = material;

            if (Application.isPlaying)
            {
                _reconstruction.Add(meshFilter.mesh);
                meshFilter.mesh.MarkDynamic();
            }
            else
            {
                meshFilter.sharedMesh = new Mesh();
                Mesh meshCopy = Mesh.Instantiate(meshFilter.sharedMesh);
                Mesh mesh = meshFilter.mesh = meshCopy;
                _reconstruction.Add(mesh);
                mesh.MarkDynamic();
            }
        }
        
        private void SplitSharedVerticesMesh(double[] reconstructionVertices, int[] reconstructionTriangles, int numOfTriangles)
        {
            _verticesSaved.Clear();
            _trianglesSaved.Clear();

            int trianglesComputed = 0;

            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            Dictionary<int, int> triangleToVertex = new Dictionary<int, int>();

            Vector3[] verticesFormatted = new Vector3[reconstructionVertices.Length / 3]; // actual verts is /3 
            for (int i = 0; i < verticesFormatted.Length; ++i)
            {
                verticesFormatted[i]
                    = new Vector3(
                        (float)reconstructionVertices[(i * 3)],
                        -(float)reconstructionVertices[(i * 3) + 1], // right -> left handed conversion
                        (float)reconstructionVertices[(i * 3) + 2]);
            }

            for (int i = 0; i < reconstructionTriangles.Length; i += 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i + j >= reconstructionTriangles.Length) // IndexOutOfRangeException: Array index is out of range.
                    {
                        continue;
                    }

                    if (!triangleToVertex.ContainsKey(reconstructionTriangles[i + j]))
                    {
                        triangleToVertex.Add(reconstructionTriangles[i + j], vertices.Count);
                        vertices.Add(verticesFormatted[reconstructionTriangles[i + j]]);
                    }
                    triangles.Add(triangleToVertex[reconstructionTriangles[i + j]]);
                }
                trianglesComputed++;

                if (trianglesComputed >= numOfTriangles)
                {
                    // set
                    _verticesSaved.Add(vertices.ToArray());
                    _trianglesSaved.Add(triangles.ToArray().Reverse().ToArray());

                    // clear
                    vertices.Clear();
                    triangles.Clear();
                    triangleToVertex.Clear();

                    trianglesComputed = 0;
                }
            }

            // the rest of the triangles
            if (vertices.Count > 0)
            {
                // set
                _verticesSaved.Add(vertices.ToArray());

                // solving the problem of non multiple of three without completly ignore all the other triangles
                if (triangles.Count % 3 != 0)
                {
                    for (int i = 0; i < triangles.Count % 3; i++)
                    {
                        triangles.RemoveAt(triangles.Count - 1);
                    }
                }

                _trianglesSaved.Add(triangles.ToArray().Reverse().ToArray());
            }
        }
    }
}