using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Updates a camera during the LateUpdate phase
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class PreRenderTexture : MonoBehaviour
    {
        private void Awake()
        {
            Camera camera = GetComponent<Camera>();

            if (camera.enabled)
            {
                Debug.LogWarning("Camera should not be enabled!");

                camera.enabled = false;
            }
        }

        private void LateUpdate()
        {
            GetComponent<Camera>().Render();
        }
    }
}