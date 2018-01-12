using System;
using System.Collections.Generic;

namespace Meta.HandInput
{

    /// <summary>
    /// Class maintaining the palm's state.
    /// </summary>
    public class PalmStateMachine
    {

        private Action _onHoverEnter, _onHoverExit;
        private Action _onGrabStart, _onGrabEnd;

        private PalmState _currentState;
        private Dictionary<PalmStateTransition, KeyValuePair<PalmState, Action>> _transitions;

        /// <summary>
        /// Current palm state getter.
        /// </summary>
        public PalmState CurrentState
        {
            get { return _currentState; }
        }

        /// <summary>
        /// Event to get fired when grab ends
        /// </summary>
        public Action OnGrabEnd
        {
            get { return _onGrabEnd; }
            set { _onGrabEnd = value; }
        }

        /// <summary>
        /// Event to get fired when grab starts
        /// </summary>
        public Action OnGrabStart
        {
            get { return _onGrabStart; }
            set { _onGrabStart = value; }
        }

        /// <summary>
        /// Event to get fired when hover state starts
        /// </summary>
        public Action OnHoverEnter
        {
            get { return _onHoverEnter; }
            set { _onHoverEnter = value; }
        }

        /// <summary>
        /// Event to get fired when hover state ends
        /// </summary>
        public Action OnHoverExit
        {
            get { return _onHoverExit; }
            set { _onHoverExit = value; }
        }

        public PalmStateMachine()
        {
            _currentState = PalmState.Idle;
        }

        private KeyValuePair<PalmState, Action> GetNext(PalmStateCommand palmStateCommand)
        {
            PalmStateTransition transition = new PalmStateTransition(_currentState, palmStateCommand);
            KeyValuePair<PalmState, Action> nextState;
            if (!_transitions.TryGetValue(transition, out nextState))
            { throw new Exception("Invalid transition: " + _currentState + " -> " + palmStateCommand); }
           
            return nextState;
        }
        

        /// <summary>
        /// Event to notify state machine to move onto next state.
        /// </summary>
        /// <param name="palmStateCommand">Command to exicute.</param>
        /// <returns>New palm state.</returns>
        public PalmState MoveNext(PalmStateCommand palmStateCommand)
        {
            var nextState = GetNext(palmStateCommand);
            _currentState = nextState.Key;
            if (nextState.Value != null)
            {
                nextState.Value.Invoke();
            }
            else
            {
                UnityEngine.Debug.Log("Nest state event is null");
            }
            return _currentState;

        }

        /// <summary>
        /// Initialization state machine
        /// </summary>
        public void Initialize()
        {
            _transitions = new Dictionary<PalmStateTransition, KeyValuePair<PalmState, Action>>
            {
                { new PalmStateTransition(PalmState.Idle, PalmStateCommand.HoverEnter), new KeyValuePair<PalmState, Action>(PalmState.Hovering, _onHoverEnter) },
                { new PalmStateTransition(PalmState.Hovering, PalmStateCommand.HoverLeave), new KeyValuePair<PalmState, Action>(PalmState.Idle, _onHoverExit) },
                { new PalmStateTransition(PalmState.Hovering, PalmStateCommand.Grab), new KeyValuePair<PalmState, Action>(PalmState.Grabbing, _onGrabStart) },
                { new PalmStateTransition(PalmState.Grabbing, PalmStateCommand.Release), new KeyValuePair<PalmState, Action>(PalmState.Hovering, _onGrabEnd) },
            };
        }


        private class PalmStateTransition
        {
            private readonly PalmState _currentState;
            private readonly PalmStateCommand _palmStateCommand;

            public PalmStateTransition(PalmState currentState, PalmStateCommand palmStateCommand)
            {
                _currentState = currentState;
                _palmStateCommand = palmStateCommand;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * _currentState.GetHashCode() + 31 * _palmStateCommand.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                PalmStateTransition other = obj as PalmStateTransition;
                return other != null && this._currentState == other._currentState && this._palmStateCommand == other._palmStateCommand;
            }
        }

    }
}