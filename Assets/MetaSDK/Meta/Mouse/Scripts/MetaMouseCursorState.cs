using System.Collections.Generic;
using UnityEngine;

namespace Meta.Mouse
{
    /// <summary>
    /// Add MetaMouseCursorState to GameObject which interacts with MetaMouse to change
    /// the MetaMouses visuals when hoviering over that GameObject.
    /// </summary>
	public class MetaMouseCursorState : MonoBehaviour
	{
        [System.Serializable]
	    private class KeyState
	    {
            [SerializeField]
            private KeySet _keySet;

            [SerializeField]
            private CursorState _stateName = CursorState.Click;

	        public KeySet KeySet
	        {
	            get { return _keySet; }
	        }

	        public CursorState StateName
	        {
	            get { return _stateName; }
	        }
	    }

	    public enum CursorState
	    {
            Click,
            ClickDrag,
            Hover,
            RotateClick,
            RotateIdle,
	        ScaleClick,
            ScaleIdle,
            Hide,
	    }

        [SerializeField]
        private List<KeyState> _keyStates;

	    public string EngagedKeyState()
	    {
	        for (int i = 0; i < _keyStates.Count; ++i)
	        {
	            if (_keyStates[i].KeySet.IsPressed())
	            {
	                return _keyStates[i].StateName.ToString();
	            }
	        }
	        return "";
	    }
	}
}