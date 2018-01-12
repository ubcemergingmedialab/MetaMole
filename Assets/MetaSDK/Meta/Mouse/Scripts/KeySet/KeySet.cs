using System;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// A set of keyboard keys that are used to perform the same action.
    /// Keys can be grouped into "chords", sets of keys that should be pressed simultaneously.
    /// </summary>
    [CreateAssetMenu(fileName = "KeySet", menuName = "KeySet", order = 20)]
    public class KeySet : ScriptableObject
    {
        /// <summary>
        /// Chords that make up the KeySet
        /// </summary>
        [SerializeField]
        private Chord[] _chords;

        /// <summary>
        /// Chords that make up the KeySet
        /// </summary>
        public Chord[] Chords
        {
            get { return _chords; }
            set { _chords = value; }
        }

        /// <summary>
        /// Checks if any of the Chords are pressed
        /// </summary>
        /// <returns></returns>
        public bool IsPressed()
        {
            return CheckChords(chord => chord.IsPressed());
        }

        /// <summary>
        /// Checks for a key down completing any of the chords
        /// </summary>
        /// <returns></returns>
        public bool GetDown()
        {
            return CheckChords(chord => chord.GetDown());
        }

        /// <summary>
        /// Checks for a key up ending any of the chords
        /// </summary>
        /// <returns></returns>
        public bool GetUp()
        {
            return CheckChords(chord => chord.GetUp());
        }

        /// <summary>
        /// Perform checks on the chords
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        private bool CheckChords(Func<Chord, bool> check)
        {
            for (int i = 0; i < _chords.Length; i++)
            {
                if (check(_chords[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}