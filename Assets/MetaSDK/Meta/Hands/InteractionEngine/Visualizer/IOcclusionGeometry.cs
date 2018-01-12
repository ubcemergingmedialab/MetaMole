using UnityEngine;
using System.Collections;

public interface IOcclusionGeometry
{
    /// <summary>
    ///     Create geometry from internal width and height values.
    /// </summary>
    void CreateGeometry();

    /// <summary>
    ///     Create a mesh of the given width and height.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <summary>
    ///     Create a mesh given a set of parameters.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="particleWidth"></param>
    /// <param name="particleHeight"></param>
    /// <param name="xOffset"></param>
    /// <param name="yOffset"></param>
    void CreateGeometry(uint width, uint height, float particleWidth = 1, float particleHeight = 1, float xOffset = 0, float yOffset = 0);

    /// <summary>
    ///     Retrieve the geometry mesh.
    /// </summary>
    /// <returns>A reference to the geometry.</returns>
    Mesh GetGeometry();

    /// </summary>
    /// Set the shader for the mesh.

    /// <summary>
    /// <param name="shaderName"></param>
    //void SetShader(string shaderName);

    /// <summary>
    /// Set the particle texture of the mesh.
    /// </summary>
    /// <param name="particleTexture"></param>
    //void SetParticleTexture(Texture2D particleTexture);

    /// <summary>
    /// Apply the depth data to the mesh via the shader.
    /// </summary>
    /// <param name="data"></param>
    //void ApplyDepthData(ushort[] data);
}
