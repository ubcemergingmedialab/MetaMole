using UnityEngine;
using System.Linq;

namespace Meta.HandInput
{
    /// <summary>
    /// Represents sdk HandData in the game world.
    /// </summary>
    public class Hand : MonoBehaviour
    {
        /// <summary>
        /// Hand which this feature belongs to.
        /// </summary>
        private HandData _handHand;

        /// <summary>
        /// List of all hand features.
        /// </summary>
        private HandFeature[] _allHandFeatures;

        /// <summary>
        /// The type.
        /// </summary>
        public HandType HandType
        {
            get { return _handHand.HandType; }
        }

        /// <summary>
        /// Interop hand datastructure.
        /// </summary>
        public HandData Data
        {
            get { return _handHand; }
        }

        /// <summary>
        /// The center position of the hand.
        /// </summary>
        public CenterHandFeature Palm
        {
            get { return _allHandFeatures.First(feature => feature is CenterHandFeature) as CenterHandFeature; }
        }

        /// <summary>
        /// The topmost position of the hand.
        /// </summary>
        public TopHandFeature Top
        {
            get { return _allHandFeatures.First(feature => feature is TopHandFeature) as TopHandFeature; }
        }

        /// <summary>
        /// Returns weather the hand is currently grabbing.
        /// </summary>
        public bool IsGrabbing
        {
            get { return _handHand.IsGrabbing; }
        }

        /// <summary>
        /// Unique Id associated with the hand.
        /// </summary>
        public int HandId
        {
            get { return _handHand.HandId; }
        }
        
        /// <summary>
        /// Initialize Hand and each of the HandFeatures.
        /// </summary>
        public void InitializeHandData(HandData handData)
        {
            _handHand = handData;

            _allHandFeatures = GetComponentsInChildren<HandFeature>();

            foreach (var handFeature in _allHandFeatures)
            {
                handFeature.Initialize(handData);
            }
        }

        /// <summary>
        /// Notifies each of the HandFeatures that the hand has become invalid.
        /// </summary>
        public void MarkInvalid()
        {
            foreach (var motionHandFeature in _allHandFeatures)
            {
                motionHandFeature.OnInvalid();
            }
        }

        /// <summary>
        /// Returns hand feature of specified type.
        /// </summary>
        /// <typeparam name="THandFeature">Requested HandFeature Type.</typeparam>
        public HandFeature GetChildHandFeature<THandFeature>() where THandFeature : HandFeature
        {
            return _allHandFeatures.First(h => h is THandFeature);
        }
    }
}