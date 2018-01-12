using UnityEngine;
using System.Collections.Generic;

namespace Meta
{

    /// <summary>
    /// The HudLock class locks GameObjects to Camera space,
    /// making them appear as if they are a part of the HUD
    /// as they won't appear to move when the camera position or rotation changes
    /// </summary>
    internal class HudLock : IEventReceiver
    {

        /// <summary>
        /// Adds the IEventReceiver functions to the delegates in order to be called from MetaManager
        /// </summary>
        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnUpdate(Update);
        }

        /// <summary>
        /// List of hud locked MetaBodies
        /// </summary>
        private List<MetaLocking> _hudLockedObjects = new List<MetaLocking>();

        /// <summary>
        /// Initial Positions of locked objects
        /// </summary>
        private Dictionary<MetaLocking, Vector3> _initialPositions = new Dictionary<MetaLocking, Vector3>();

        /// <summary>
        /// Initial Rotations of locked objects
        /// </summary>
        private Dictionary<MetaLocking, Quaternion> _initialRotations = new Dictionary<MetaLocking, Quaternion>();

        /// <summary>
        /// Adds MetaBodies to the list of lockables
        /// </summary>
        internal void AddHudLockedObject(MetaLocking hudLockedObject)
        {
            if (!_hudLockedObjects.Contains(hudLockedObject))
            {
                _hudLockedObjects.Add(hudLockedObject);
                _initialPositions[hudLockedObject] = Camera.main.transform.InverseTransformPoint(hudLockedObject.transform.position);
                _initialRotations[hudLockedObject] = Quaternion.Inverse(Camera.main.transform.rotation) * hudLockedObject.transform.rotation;
            }
        }

        /// <summary>
        /// Removes MetaBodies from the list of lockables
        /// </summary>
        internal void RemoveHudLockedObject(MetaLocking hudLockedObject)
        {
            if (_hudLockedObjects.Contains(hudLockedObject))
            {
                _hudLockedObjects.Remove(hudLockedObject);
                _initialPositions.Remove(hudLockedObject);
                _initialRotations.Remove(hudLockedObject);
            }
        }

        private void Update()
        {
            UpdateHUDLocks();
        }

        /// <summary>
        /// Updates the position and rotation of the MetaBodies so that it remains locked to the HUD
        /// </summary>
        private void UpdateHUDLocks()
        {
            foreach (MetaLocking MetaLocking in _hudLockedObjects)
            {
                if (MetaLocking != null)
                {
                    Vector3 pos = Camera.main.transform.TransformPoint(_initialPositions[MetaLocking]);
                    Vector3 rot = (Camera.main.transform.rotation * _initialRotations[MetaLocking]).eulerAngles;
                    if (MetaLocking.useDefaultHUDSettings)
                    {
                        MetaLocking.transform.position = pos;
                        MetaLocking.transform.rotation = Quaternion.Euler(rot);
                    }
                    else
                    {
                        if (MetaLocking.hudLockPosition)
                        {
                            if (!MetaLocking.hudLockPositionX)
                            {
                                pos.x = MetaLocking.transform.position.x;
                            }
                            if (!MetaLocking.hudLockPositionY)
                            {
                                pos.y = MetaLocking.transform.position.y;
                            }
                            if (!MetaLocking.hudLockPositionZ)
                            {
                                pos.z = MetaLocking.transform.position.z;
                            }
                            MetaLocking.transform.position = pos;
                        }
                        if (MetaLocking.hudLockRotation)
                        {
                            if (!MetaLocking.hudLockRotationX)
                            {
                                rot.x = MetaLocking.transform.rotation.eulerAngles.x;
                            }
                            if (!MetaLocking.hudLockRotationY)
                            {
                                rot.y = MetaLocking.transform.rotation.eulerAngles.y;
                            }
                            if (!MetaLocking.hudLockRotationZ)
                            {
                                rot.z = MetaLocking.transform.rotation.eulerAngles.z;
                            }
                            MetaLocking.transform.rotation = Quaternion.Euler(rot);
                        }
                    }
                }
            }
        }

    }

}