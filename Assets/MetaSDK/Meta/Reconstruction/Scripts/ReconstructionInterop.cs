using System;
using System.Runtime.InteropServices;

namespace Meta
{
    /// <summary>
    /// This module exposes the meta 3D Reconstruction module 
    /// It provides access to a 3D spatial map created by the headset 
    /// </summary>
    public class ReconstructionInterop
    {
        /// <summary>
        /// Connect to reconstruction module.
        /// </summary>
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "connectReconstruction")]
        public static extern void ConnectReconstruction();
        
        /// <summary>
        /// Start integrating depth images 
        /// </summary>
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "startReconstruction")]
        public static extern void StartReconstruction();

        /// <summary>
        /// Toggles pause integrating depth images.
        /// </summary>
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "pauseReconstruction")]
        public static extern void PauseReconstruction();

        /// <summary>
        /// End integration of depth images.
        /// </summary>
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "endReconstruction")]
        public static extern void EndReconstruction();

        /// <summary>
        /// Reset the reconstruction module, clears mesh.
        /// Allows you to rescan.
        /// </summary>
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "resetReconstruction")]
        public static extern void ResetReconstruction();

        /// <summary>
        /// Internal API for saving the reconstruction as a .ply file
        /// </summary>
        /// <param name="filename">filename with .ply</param>
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "saveReconstruction")]
        private static extern void saveReconstruction([MarshalAs(UnmanagedType.BStr)] string filename);

        // TODO: better interfaces for retrieving Meshes
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "getReconstructionMesh")]
        public static extern void GetReconstructionMesh(
            out IntPtr verts,
            out int num_verts,
            out IntPtr indices,
            out int num_tris
            );

        /// <summary>
        /// Save 3D reconstruction as .ply file
        /// </summary>
        /// <param name="filename">filename without .ply</param>
        public static void SaveReconstruction(string filename)
        {
            saveReconstruction(filename + ".ply");
        }
    }
}