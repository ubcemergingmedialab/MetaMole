using System;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Apply alignment user settings to the appropriate transforms.
    /// This component may be attached to any game object in the MetaCameraRig 
    /// hierarchy. The LeftCamera and RightCamera member variables must be set 
    /// to the left and right cameras of the MetaCameraRig accordingly.
    /// </summary>
    public class AlignmentUserSettings : MetaBehaviour, IAlignmentUpdateListener
    {
        [SerializeField]
        private Transform LeftCamera;

        [SerializeField]
        private Transform RightCamera;

        /// <summary>
        /// Set at runtime - the default left camera position.
        /// </summary>
        private Vector3 _defaultLeftCameraPos;

        /// <summary>
        /// Set at runtime - the default right camera position.
        /// </summary>
        private Vector3 _defaultRightCameraPos;

        /// <summary>
        /// Has an alignment update occured since the last frame
        /// </summary>
        private bool _alignmentUpdateOccured = false;

        // Use this for initialization
        private void Start()
        {
            _SetDefaultCameraPositions();

            //Register this 
            if (metaContext != null && metaContext.ContainsModule<AlignmentHandler>())
            {
                AlignmentHandler handler = metaContext.Get<AlignmentHandler>();
                handler.AlignmentUpdateListeners.Add(this);
            }

            _LoadCameraPositions();
        }

        /// <summary>
        /// Sets the default camera postions. This allows the user to specify the default positions in the editor.
        /// </summary>
        private void _SetDefaultCameraPositions()
        {
            _defaultLeftCameraPos = (LeftCamera == null) ? Vector3.zero: LeftCamera.localPosition;
            _defaultRightCameraPos = (RightCamera == null) ? Vector3.zero : RightCamera.localPosition;
        }

        /// <summary>
        /// Attempts to load the camera positions from the alignment profile stored in the metacontext.
        /// </summary>
        private void _LoadCameraPositions()
        {
            if (LeftCamera != null && RightCamera != null)
            {
                AlignmentProfile profile = null;
                if (metaContext != null && metaContext.ContainsModule<AlignmentProfile>())
                {
                    profile = metaContext.Get<AlignmentProfile>();
                }

                if (profile != null)
                {
                    LeftCamera.localPosition = profile.EyePositionLeft;
                    RightCamera.localPosition = profile.EyePositionRight;
                }
                else
                {
                    LeftCamera.localPosition = _defaultLeftCameraPos;
                    RightCamera.localPosition = _defaultRightCameraPos;
                }
            }
        }

        private void Update()
        {
            //The use of _alignmentUpdateOccured is a hack in order to modify transform positions
            // in Unity's main thread. Modifying transforms in other threads will not work.
            if (_alignmentUpdateOccured)
            {
                _LoadCameraPositions();
                _alignmentUpdateOccured = false;
            }
        }

        /// <summary>
        /// Sets a flag to load the camera positions from the alignment profile provided in the parameter at a later time.
        /// </summary>
        /// <param name="newProfile">the profile</param>
        public void OnAlignmentUpdate(AlignmentProfile newProfile)
        {
            //This is a hack in order to load transformations at a later time during Unity's main thread of execution.
            _alignmentUpdateOccured = true;
        }
    }
}
