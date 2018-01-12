using UnityEngine;
using meta.types;
using HandData = Meta.HandInput.HandData;

namespace Meta
{
    internal class HandsModule : IEventReceiver
    {
        /// <summary> Specified weather the module is initialized. /// </summary>
        internal bool Initialized { private get; set; }
        /// <summary> Datastructure containing all data for right hand. /// </summary>
        public HandData RightHand { get; private set; }
        /// <summary> Datastructure containing all data for left hand. /// </summary>
        public HandData LeftHand { get; private set; }

        public System.Action<HandData> OnHandEnterFrame;
        public System.Action<HandData> OnHandExitFrame;

        private FrameHands _frame;
        private bool _recievedFirstFrame = false;
        private const int kBuffMaxSize = 4000;
        private byte[] _buffer = new byte[kBuffMaxSize];

        /// <summary>
        /// Container for all hands related data for current camera frame.
        /// </summary>
        internal FrameHands? Frame
        {
            get { return _recievedFirstFrame ? _frame : (FrameHands?)null; }
        }

        public HandsModule(Transform handsOrigin)
        {
            // Initialize Hands Datastructures.

            RightHand = new HandData(handsOrigin);
            LeftHand = new HandData(handsOrigin);

            // Subscribe to hand events.

            RightHand.OnEnterFrame += () =>
            {
                if (OnHandEnterFrame != null)
                {
                    OnHandEnterFrame.Invoke(RightHand);
                }
            };
            LeftHand.OnEnterFrame += () =>
            {
                if (OnHandEnterFrame != null)
                {
                    OnHandEnterFrame.Invoke(LeftHand);
                }
            };

            RightHand.OnExitFrame += () =>
            {
                if (OnHandExitFrame != null)
                {
                    OnHandExitFrame.Invoke(RightHand);
                }
            };
            LeftHand.OnExitFrame += () =>
            {
                if (OnHandExitFrame != null)
                {
                    OnHandExitFrame.Invoke(LeftHand);
                }
            };
        }

        private void Update()
        {
            if (!Initialized) return;

            if (MetaCocoInterop.GetFrameHandsFlatbufferObject(ref _buffer, out _frame))
            {
                _recievedFirstFrame = true;

                meta.types.HandData? incomingRight = null;
                meta.types.HandData? incomingLeft = null;
                for (int i = 0; i < _frame.HandsLength; i++)
                {
                    switch (_frame.Hands(i).Value.HandType)
                    {
                        case meta.types.HandType.RIGHT:
                            incomingRight = _frame.Hands(i);
                            break;
                        default:
                            incomingLeft = _frame.Hands(i);
                            break;
                    }
                }

                RightHand.UpdateHand(incomingRight);
                LeftHand.UpdateHand(incomingLeft);

                RightHand.UpdateEvents();
                LeftHand.UpdateEvents();
            }
        }

        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnUpdate(Update);
        }
    }
}