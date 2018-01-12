using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Load/Save meshes from/to disk
    /// </summary>
    public interface IModelFileManipulator
    {
        /// <summary>
        /// Loads the mesh data from a given file saved on disk
        /// </summary>
        /// <param name="filepath">Complete or relative path to the file</param>
        /// <returns>The loaded mesh data.</returns>
        MeshData LoadMeshFromFile(string filepath);

        /// <summary>
        /// Save vertices and faces of a mesh as a model file
        /// </summary>
        /// <param name="filename">Name of the file created after the saving process</param>
        /// <param name="vertices">Vertices of the mesh</param>
        /// <param name="triangles">Triangles of the mesh</param>
        void SaveMeshToFile(string filename, Vector3[] vertices, int[] triangles);
    }
}