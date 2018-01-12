using UnityEngine;

namespace Meta.HandInput
{
    [System.Serializable]
    public class HandData
    {
        private const float kMaxUntrackedTime = 1.55f;
        private const float kMaxAnglesFromGaze = 32.5f;

        /// <summary> Depth camera transform object </summary>
        private readonly Transform _handsOriginTransform;

        /// <summary> Unique id for hand </summary>
        public int HandId { get; private set; }
        /// <summary> Hand's top point </summary>
        public Vector3 Top { get; private set; }
        /// <summary> Hand's palm anchor </summary>
        public Vector3 Palm { get; private set; }
        /// <summary> Hand's grab anchor </summary>
        public Vector3 GrabAnchor { get; private set; }
        /// <summary> Hand's grab value </summary>
        public bool IsGrabbing { get; private set; }
        /// <summary> hand's HandType </summary>
        public HandType HandType { get; private set; }
        /// <summary> Is the hand visible is the cameras view. </summary>
        public bool IsTracked { get; private set; }

        private bool _wasTracked;
        private bool _untrackedInView;
        private float _timeLostTracking;

        /// <summary> Event to get fired whenever the hand has entered the camera's view. /// </summary>
        public System.Action OnEnterFrame;
        /// <summary> Event to get fired whenever the hand has left the camera's view. /// </summary>
        public System.Action OnExitFrame;
        /// <summary> Event to get fired whenever the tracking of the hand is lost. /// </summary>
        public System.Action OnTrackingLost;
        /// <summary> Event to get fired whenever the tracking of the hand is recovered. /// </summary>
        public System.Action OnTrackingRecovered;

        /// <summary> Returns the angle between the gaze vector and the palm-to-sensor vector. </summary>
        private float AnglesFromGaze
        {
            get
            {
                var palmToSensorDir = (Palm - _handsOriginTransform.transform.position).normalized;
                return Vector3.Angle(_handsOriginTransform.forward, palmToSensorDir);
            }
        }

        public HandData(Transform origin)
        {
            _handsOriginTransform = origin;
        }

        /// <summary>
        /// Applies hand properties from input meta.types.HandData to current hand.
        /// </summary>
        public void UpdateHand(meta.types.HandData? cocoHand)
        {
            _wasTracked = IsTracked;

            if (_untrackedInView)
            {
                if (cocoHand.HasValue || Time.time - _timeLostTracking > kMaxUntrackedTime) 
                {
                    _untrackedInView = false;
                }
            }
            else if (!cocoHand.HasValue && AnglesFromGaze < kMaxAnglesFromGaze)
            {
                _untrackedInView = true;
                _timeLostTracking = Time.time;
            }

            if (_untrackedInView)
            {
                return;
            }


            if (cocoHand.HasValue)
            {
                var hand = cocoHand.Value;
                var localToWorldMatrix = _handsOriginTransform.localToWorldMatrix;

                HandId = hand.HandId;
                HandType = hand.HandType == meta.types.HandType.RIGHT ? HandType.Right : HandType.Left;
                IsGrabbing = hand.IsGrabbing;
                GrabAnchor = localToWorldMatrix.MultiplyPoint3x4(hand.GrabAnchor.Value.ToVector3());
                Palm = localToWorldMatrix.MultiplyPoint3x4(hand.HandAnchor.Value.ToVector3());
                Top = localToWorldMatrix.MultiplyPoint3x4(hand.Top.Value.ToVector3());
                IsTracked = true;
            }
            else
            {
                IsGrabbing = false;
                IsTracked = false;
            }
        }

        /// <summary>
        /// Fires all hand related events. 
        /// Called after all hands in view are updated.
        /// </summary>
        public void UpdateEvents()
        {
            if (_wasTracked != IsTracked)
            {
                if (IsTracked)
                {
                    if (OnEnterFrame != null)
                    {
                        OnEnterFrame.Invoke();
                    }
                }
                else
                {
                    if (OnExitFrame != null)
                    {
                        OnExitFrame.Invoke();
                    }
                }
            }
        }

        public override string ToString()
        {
            string data;
            data  = "Hand Type: " + (HandType == HandType.Right ? "Right" : "Left");
            data += "\nHand Id: " + HandId;
            data += "\nIs Grabbed: " + (IsGrabbing ? "True" : "False");
            data += "\nIs Tracked: " + (IsTracked ? "True" : "False");
            return data;
        }

    };
}
