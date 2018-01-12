using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Meta.Internal
{
    /// <summary>   A native depth texture handler. </summary>
    class NativeDepthTextureHandle
    {
        public Texture2D tex;

        /// <summary>   Creates texture and pass to plugin. </summary>
        /// <param name="height">   The height. </param>
        /// <param name="width">    The width. </param>
        public void CreateTextureAndPassToPlugin(int height, int width)
        {
            // Create a texture
            tex = new Texture2D(height, width, TextureFormat.RGBAFloat, false, true);

            // Use bilinear, point caused issues with striations in the depth image
            // bilinear also causes issues but they are handled in the DepthMapOcclusion.shader
            tex.filterMode = FilterMode.Bilinear;

            // Call Apply() so it's actually uploaded to the GPU
            tex.Apply();

            // Pass texture pointer to the plugin
            HandKernelInterop.SetTextureFromUnity(tex.GetNativeTexturePtr(), height, width);
        }
    }
}
