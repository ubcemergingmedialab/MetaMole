using UnityEngine;
using System;

namespace Meta
{
    /// <summary>
    /// A set of keys where only one key needs to be pressed
    /// </summary>
    [Serializable]
    public class Subchord
    {
        [SerializeField]
        private KeyCode[] _keys;

        private IKeyboardWrapper _keyboardWrapper;

        /// <summary>
        /// The set of keys to check. If any of the keys meet the conditions of an event, the event check function
        /// returns true.
        /// </summary>
        public KeyCode[] Keys
        {
            get { return _keys; }
        }

        private IKeyboardWrapper KeyboardWrapper
        {
            get { return _keyboardWrapper ?? (_keyboardWrapper = GameObject.FindObjectOfType<MetaContextBridge>().CurrentContext.Get<IKeyboardWrapper>()); }
        }

        /// <summary>
        /// Check if one of the keys is pressed
        /// </summary>
        public bool IsPressed()
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                if (KeyboardWrapper.GetKey(_keys[i]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if one of the keys had a key up
        /// </summary>
        /// <returns></returns>
        public bool GetUp()
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                if (KeyboardWrapper.GetKeyUp(_keys[i]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if one of the keys had a key down
        /// </summary>
        public bool GetDown()
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                if (KeyboardWrapper.GetKeyDown(_keys[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
