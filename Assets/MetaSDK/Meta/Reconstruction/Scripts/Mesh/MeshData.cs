using System.Collections.Generic;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Data structure for raw mesh data
    /// </summary>
    public class MeshData
    {
        public List<Vector3> Vertices { get; private set; }
        public List<int> Triangles { get; private set; }
        public string Name { get; private set; }

        public MeshData(List<Vector3> vertices, List<int> triangles, string name)
        {
            Vertices = vertices;
            Triangles = triangles;
            Name = name;
        }
    }
}