using UnityEngine;
using System.Collections;
using Meta.Internal;

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary>   Manager for depth occlusions. </summary>
/// <seealso cref="T:UnityEngine.MonoBehaviour" />
////////////////////////////////////////////////////////////////////////////////////////////////////
public class DepthOcclusionManager : MonoBehaviour
{
    /// <summary>   true if coroutine started. </summary>
    private bool _coroutineStarted;
    /// <summary>   The depth occlusion handler. </summary>
    internal DepthOcclusionHandler depthOcclusionHandler;

    // Use this for initialization
    public void Update()
    {
        if ((depthOcclusionHandler != null) && depthOcclusionHandler.initlialzed && !_coroutineStarted)
        {
            StartCoroutine(depthOcclusionHandler.CallPluginAtEndOfFrames()); //???
            _coroutineStarted = true;
        }
    }

    /// <summary>   Executes the disable action. </summary>
    public void OnDisable()
    {
        //To be able to restart the coroutine when the GameObject is disabled and re enabled.
        _coroutineStarted = false;
    }
}
