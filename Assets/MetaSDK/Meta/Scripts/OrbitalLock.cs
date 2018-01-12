using UnityEngine;
using System.Collections.Generic;

namespace Meta
{

    /// <summary>
    /// The OrbitalLock class makes GameObjects look at the MainCamera and
    /// locks GameObjects at a constant distance away from the MainCamera
    /// </summary>
    internal class OrbitalLock : IEventReceiver
    {

        /// <summary>
        /// The camera's position in the last frame
        /// </summary>
        private Vector3? _oldCameraPos = null;

        private static readonly float epsilon = 0.001f;

        /// <summary>
        /// The lock distance when useDefaultOrbitalSettings = true
        /// </summary>
        // private float _defaultLockDistance = 0.4f;  // TODO: actually use this distance, or remove the useDefaultOrbitalSettings flag
        /// <summary>
        /// List of orbit locked MetaBodies
        /// </summary>
        private List<MetaLocking> _orbitLockedObjects = new List<MetaLocking>();

        /// <summary>
        /// Adds MetaBodies to the list of lockables
        /// </summary>
        internal void AddOrbitalLockedObject(MetaLocking orbitLockedObject)
        {
            if (!_orbitLockedObjects.Contains(orbitLockedObject))
            {
                _orbitLockedObjects.Add(orbitLockedObject);
            }
        }

        /// <summary>
        /// MetaBodies from the list of lockables
        /// </summary>
        internal void RemoveOrbitalLockedObject(MetaLocking orbitLockedObject)
        {
            if (_orbitLockedObjects.Contains(orbitLockedObject))
            {
                _orbitLockedObjects.Remove(orbitLockedObject);
            }
        }

        /// <summary>
        /// Adds the IEventReceiver functions to the delegates in order to be called from MetaManager
        /// </summary>
        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnUpdate(Update);
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateOrbitalLocks();
        }

        /// <summary>
        /// Updates the position and rotations of the orbital locked objects
        /// so that they are at the lock distance away from the camera and look at the camera
        /// </summary>
        private void UpdateOrbitalLocks()
        {
            var cameraPos = Camera.main.transform.position;
            foreach (MetaLocking metaLocking in _orbitLockedObjects)
            {
                if (metaLocking != null)
                {
                    if (metaLocking.useDefaultOrbitalSettings || metaLocking.orbitalLockDistance)
                    {
                        //at least one frame must have passed to have the old camera position.
                        if (_oldCameraPos.HasValue)
                        {
                            // This is the primary method of updating the target object's location relative to
                            // the meta2.  
                            var delta = (cameraPos - _oldCameraPos.Value);
                            metaLocking.transform.position += delta;
                        }

                        //The difference between the true distance of the orbit gameobject and the meta2 camera and the desired distance.
                        var magnitudeDifference = Mathf.Abs((metaLocking.transform.position - cameraPos).magnitude - metaLocking.lockDistance);
                        if (magnitudeDifference > epsilon)
                        {
                            // Only modify the target object's position relative to the camera along a vector with precision
                            // issues if the target object is not the desired distance from the camera. 
                            // This usually happens if the user moves the target object, and should not happen frequently
                            // when the camera moves. 
                            // This FeatureType is designed to prevent the position of the target object from changing relative
                            //  to the meta2 when the meta2 is moved.
                            Vector3 lookVector = (metaLocking.transform.position - cameraPos).normalized;
                            metaLocking.transform.position = (lookVector * metaLocking.lockDistance + cameraPos);
                        }
                    }
                    if (metaLocking.useDefaultOrbitalSettings || metaLocking.orbitalLookAtCamera)
                    {
                        Vector3 lookVector = Camera.main.transform.position - metaLocking.transform.position;
                        Quaternion lookRotation = Quaternion.LookRotation(lookVector);
                        if (metaLocking.useDefaultOrbitalSettings || metaLocking.orbitalLookAtCameraFlipY)
                        {
                            lookRotation *= Quaternion.Euler(new Vector3(0, 180, 0));
                        }
                        if (metaLocking.transform.rotation != lookRotation)
                        {
                            metaLocking.transform.rotation = lookRotation;
                        }
                    }
                }
            }

            _oldCameraPos = cameraPos;
        }
    }
}