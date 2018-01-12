namespace Meta
{
    /// <summary>
    /// Declares how to subscribe to the Virtual Webcam Mirror Mode state change.
    /// Implementors which are also Monobehaviours and a component of the MetaCameraRig 
    /// are registered automatically. 
    /// </summary>
    public interface IWebcamStateChangeListener
    {

        /// <summary>
        /// For when the Mirror Mode state of the Virtual Webcam changes.
        /// </summary>
        /// <param name="changedToMode"></param>
        void OnStateChanged(WebcamMirrorModes changedToMode);

    }
}


