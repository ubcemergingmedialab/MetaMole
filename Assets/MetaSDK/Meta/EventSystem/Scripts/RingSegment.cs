using Meta.Extensions;
using Meta.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Meta
{
    /// <summary>
    /// Procedurally constructs a mesh in the shape of a slice of a ring
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class RingSegment : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [FormerlySerializedAs("arcDegrees")]
        [SerializeField]
        [Range(0, 360)]
        private float _arcDegrees = 20;

        /// <summary>
        /// 
        /// </summary>
        [FormerlySerializedAs("startDegree")]
        [SerializeField]
        [Range(0, 360)]
        private float _startDegree = 0;

        /// <summary>
        /// 
        /// </summary>
        [FormerlySerializedAs("thickness")]
        [SerializeField]
        [Range(0, 1)]
        private float _thickness = 0.1f;

        /// <summary>
        /// 
        /// </summary>
        [FormerlySerializedAs("alpha")]
        [SerializeField]
        [Range(0, 1)]
        private float _alpha = 1;

        /// <summary>
        /// 
        /// </summary>
        public float Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float StartDegree
        {
            get { return _startDegree; }
            set { _startDegree = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float ArcDegrees
        {
            get { return _arcDegrees; }
            set { _arcDegrees = value; }
        }

        private void Awake()
        {
            RebuildMeshIfNeeded();
        }

        private void Update()
        {
            Mesh mesh = RebuildMeshIfNeeded();
            int arcVertexCount = Mathf.Max((int)(_arcDegrees / 2f), 2);
            float inner = Mathf.Clamp01(1 - _thickness / transform.lossyScale.x);

            ProceduralMeshUtility.BuildRingArc(mesh, _arcDegrees, inner, arcVertexCount, _startDegree - _arcDegrees / 2);

            if (Application.isPlaying)
            {
                GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color.SetAlpha(_alpha);
            }
        }

        private Mesh RebuildMeshIfNeeded()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();

            if (meshFilter.sharedMesh != null)
            {
                return meshFilter.sharedMesh;
            }

            Mesh mesh = new Mesh { hideFlags = HideFlags.HideAndDontSave };
            mesh.MarkDynamic();
            meshFilter.sharedMesh = mesh;

            return mesh;
        }
    }
}