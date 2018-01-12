using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Responsible for genetating meshes from raw vertices and triangles data
    /// </summary>
    public interface IMeshGenerator
    {
        /// <summary>
        /// Material that is assigned to the meshes generated
        /// </summary>
        Material Material { get; set; }

        /// <summary>
        /// Parent of the mesh GameObjects generated
        /// </summary>
        Transform Parent { get; set; }

        /// <summary>
        /// Generates the meshes according to the list of vertices and triangles.
        /// </summary>
        /// <param name="reconstructionVertices"></param>
        /// <param name="reconstructionTriangles"></param>
        void UpdateMeshes(double[] reconstructionVertices, int[] reconstructionTriangles);

        /// <summary>
        /// Reset the meshes data
        /// </summary>
        void ResetMeshes();
    }
}