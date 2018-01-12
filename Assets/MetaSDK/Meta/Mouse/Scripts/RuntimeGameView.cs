using UnityEngine;
using System;

namespace Meta.Mouse
{
    /// <summary>
    /// Abstraction of Unity Gameview.
    /// </summary>
    internal class RuntimeGameView
    {
        public event Action PointerEnter;
        public event Action PointerExit;
        public event Action PointerHover;
        public event Action PointerExternal;
        private const int ScreenPadding = 4;
        private readonly IPlatformMouse _platformMouse;
        private readonly IInputWrapper _inputWrapper;
        private bool _priorPointerInGameView;
        private bool _priorPointExternalGameView;
        private Rect _globalGameViewRect = new Rect();

        /// <summary>
        /// The rect of the RuntimeGameView in global, system, coordinates.
        /// </summary>
        public Rect GlobalGameViewRect
        {
            get { return _globalGameViewRect; }
        }

        public RuntimeGameView(IInputWrapper inputWrapper)
        {
            _inputWrapper = inputWrapper;
            _platformMouse = PlatformMouseFactory.GetPlatformMouse();
            _priorPointerInGameView = true;
            //Must be locked initially because CheckVisibility relies on cursor being inside
            //game view intiaily to calculate proper globalGameViewRect.
            Cursor.lockState = CursorLockMode.Locked;
        }

        /// <summary>
        /// Runs logic to determine pointer enter, exit and hover events.
        /// </summary>
        public void ProcessEvents(Vector2 pointerPosition)
        {
            bool currentCursorInGameView = false;
            if (_priorPointerInGameView)
            {
                CalculateGlobalGameViewRect();
                Rect screenRect = _inputWrapper.GetScreenRect();
                screenRect.xMin += ScreenPadding;
                screenRect.xMax -= ScreenPadding;
                screenRect.yMin += ScreenPadding;
                screenRect.yMax -= ScreenPadding;
                currentCursorInGameView = screenRect.Contains(pointerPosition);
            }
            else
            {
                currentCursorInGameView = PointerInGlobalGameView(ScreenPadding * 2);
            }
            if (currentCursorInGameView && !_priorPointerInGameView && _priorPointExternalGameView)
            {
                if (PointerEnter != null)
                {
                    PointerEnter();
                }
                _priorPointerInGameView = true;
                _priorPointExternalGameView = false;
            }
            else if (currentCursorInGameView && _priorPointerInGameView)
            {
                if (PointerHover != null)
                {
                    PointerHover();
                }
            }
            else if (!currentCursorInGameView && _priorPointerInGameView)
            {
                if (PointerExit != null)
                {
                    PointerExit();
                }
                _priorPointerInGameView = false;
            }
            else if (!currentCursorInGameView && !_priorPointerInGameView)
            {
                if (PointerExternal != null)
                {
                    PointerExternal();
                }
                _priorPointExternalGameView = true;
            }
        }

        /// <summary>
        /// Set the system pointer position relative to the RuntimeGameView.
        /// </summary>
        public void SetGlobalPointerPosRelativeGameView(Vector2 position)
        {
            position.x += _globalGameViewRect.x;
            position.y = (_globalGameViewRect.height - position.y) + _globalGameViewRect.y;
            _platformMouse.SetGlobalCursorPos(position);
        }

        /// <summary>
        /// Get the pointer position relative to the RuntimeGameView.
        /// </summary>
        public Vector2 GetGlobalPointerPosRelativeGameView()
        {
            Vector2 pos = new Vector2();
            Vector2 globalPosition = _platformMouse.GetGlobalCursorPos();
            pos.x = globalPosition.x - _globalGameViewRect.x;
            pos.y = _globalGameViewRect.height - (globalPosition.y - _globalGameViewRect.y);
            return pos;
        }

        /// <summary>
        /// Determines whether or not pointer is in RuntimeGameView.
        /// </summary>
        private bool PointerInGlobalGameView(float padding)
        {
            Rect globalScreenRect = _globalGameViewRect;
            globalScreenRect.xMin += padding;
            globalScreenRect.xMax -= padding;
            globalScreenRect.yMin += padding;
            globalScreenRect.yMax -= padding;
            return globalScreenRect.Contains(_platformMouse.GetGlobalCursorPos());
        }

        /// <summary>
        /// Calculates the global game view rect.
        /// </summary>
        /// <remarks>
        /// This works on the assumption that GetGlobalCursorPos and _inputWrapper.GetMousePosition()
        /// are on the exact same point on the screen, just one in global space and the other in local.
        /// Thus _globalScreenRect must be stored as a class variable as it can only be calculated when
        /// the point is inside the scene view. This does mean that if the pointer never enters the scene
        /// view then it cannot properly calculate, this is alleviated by starting the scene with the cursor
        /// locked which will initially force the pointer to the center of the scene view.
        /// </remarks>
        private void CalculateGlobalGameViewRect()
        {
            Vector2 globalPosition = _platformMouse.GetGlobalCursorPos();
            Vector2 localPosition = _inputWrapper.GetMousePosition();
            _globalGameViewRect.x = globalPosition.x - localPosition.x;
            _globalGameViewRect.y = globalPosition.y - (_inputWrapper.GetScreenRect().height - localPosition.y);
            _globalGameViewRect.width = _inputWrapper.GetScreenRect().width;
            _globalGameViewRect.height = _inputWrapper.GetScreenRect().height;
        }
    }
}