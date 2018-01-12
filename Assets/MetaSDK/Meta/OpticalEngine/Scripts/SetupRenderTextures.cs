using UnityEngine;

internal class SetupRenderTextures : MonoBehaviour
{
    [Range(1.0f, 4.0f)]
    [SerializeField]
    private float UpscaleAmount = 1.0f; // TODO: Check what value we need.
    [SerializeField]
    private AAValueType AAValue = AAValueType.One; // TODO: Check what value we need.
    [SerializeField]
    private Camera[] Cameras = null;
    [SerializeField]
    private GameObject[] Materials = null;

    private Resolution currentResolution;

    void Start()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        currentResolution = new Resolution();
        currentResolution.width = Screen.width;
        currentResolution.height = Screen.height;
        SetCameraAndMaterialRenderTexture(Cameras, Materials, CreateRenderTexture());
    }

    void Update()
    {
        if (currentResolution.width != Screen.width ||
            currentResolution.height != Screen.height)
        {
            currentResolution.width = Screen.width;
            currentResolution.height = Screen.height;
            SetCameraAndMaterialRenderTexture(Cameras, Materials, CreateRenderTexture());
        }
    }

    private void SetCameraAndMaterialRenderTexture(Camera[] cameras, GameObject[] materials, RenderTexture renderTexture)
    {
        foreach (var mat in materials)
        {
            mat.GetComponent<Renderer>().material.mainTexture = renderTexture;
        }
        foreach (var cam in cameras)
        {
            // TODO: figure this out properly.  We really should be releasing those old render textures! --AHG
            //RenderTexture oldRenderTexture = cam.targetTexture;
            cam.targetTexture = renderTexture;

            // Theoretically correct, but throws exceptions.
            /*if (oldRenderTexture != null)
            {
                oldRenderTexture.ReleaseToSeat();
            }*/
        }
    }

    private RenderTexture CreateRenderTexture()
    {
        RenderTexture renderTexture = new RenderTexture(
            (int)(currentResolution.width * UpscaleAmount),
            (int)(currentResolution.height * UpscaleAmount),
            24,
            RenderTextureFormat.ARGB32,
            RenderTextureReadWrite.Default);
        renderTexture.antiAliasing = (int)AAValue;
        renderTexture.wrapMode = TextureWrapMode.Clamp;
        renderTexture.filterMode = FilterMode.Bilinear;
        renderTexture.autoGenerateMips = false;
        renderTexture.anisoLevel = 0;
        renderTexture.Create();

        //Debug.Log(renderTexture.width + "x" + renderTexture.height);

        return renderTexture;
    }

    private enum AAValueType
    {
        One = 1,
        Two = 2,
        Four = 4,
        Eight = 8,
    }
}
