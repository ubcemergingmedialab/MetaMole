using System.Collections.Generic;
using UnityEngine;

namespace Meta.EditorUtils
{
    /// <summary>
    /// Handles in a better way the colors of the GUI
    /// </summary>
    public class ColorStack
    {
        private Stack<Color> _mainColorStack;
        private Stack<Color> _backgroundColorStack;
        private Stack<Color> _contentColorStack;
        private bool _defaults = false;

        /// <summary>
        /// Create a new instance of this object
        /// </summary>
        public ColorStack()
        {
            _mainColorStack = new Stack<Color>();
            _backgroundColorStack = new Stack<Color>();
            _contentColorStack = new Stack<Color>();
        }

        /// <summary>
        /// Collect the default values of the GUI.
        /// This happens onle once, further calls will do nothing.
        /// </summary>
        public void CollectDefaults()
        {
            if (_defaults)
            {
                return;
            }

            _mainColorStack.Push(GUI.color);
            _backgroundColorStack.Push(GUI.backgroundColor);
            _contentColorStack.Push(GUI.contentColor);
            _defaults = true;
        }

        #region Content Color
        /// <summary>
        /// Push the given color to the Content Stack.
        /// This will update the GUI.contentColor to the given value
        /// </summary>
        /// <param name="color">Color of Content</param>
        public void PushContent(Color color)
        {
            _contentColorStack.Push(color);
            GUI.contentColor = color;
        }

        /// <summary>
        /// Pop the color of the Content Stack.
        /// This will update the GUI.contentColor to the previous value.
        /// </summary>
        /// <returns>Color Popped</returns>
        public Color PopContent()
        {
            if (_contentColorStack.Count <= 0)
            {
                return Color.white;
            }
            var color = _contentColorStack.Pop();
            GUI.contentColor = _contentColorStack.Peek();
            return color;
        }
        #endregion

        #region Background Color
        /// <summary>
        /// Push the given color to the Background Stack.
        /// This will update the GUI.backgroundColor to the given value
        /// </summary>
        /// <param name="color">Color of Background</param>
        public void PushBackground(Color color)
        {
            _backgroundColorStack.Push(color);
            GUI.backgroundColor = color;
        }

        /// <summary>
        /// Pop the color of the Background Stack.
        /// This will update the GUI.backgroundColor to the previous value.
        /// </summary>
        /// <returns>Color Popped</returns>
        public Color PopBackground()
        {
            if (_backgroundColorStack.Count <= 0)
            {
                return Color.white;
            }
            var color = _backgroundColorStack.Pop();
            GUI.backgroundColor = _backgroundColorStack.Peek();
            return color;
        }
        #endregion

        #region Main Stack
        /// <summary>
        /// Push the given color to the Main Stack.
        /// This will update the GUI.color to the given value
        /// </summary>
        /// <param name="color"></param>
        public void Push(Color color)
        {
            _mainColorStack.Push(color);
            GUI.color = color;
        }

        /// <summary>
        /// Pop the color of the Main Stack.
        /// This will update the GUI.color to the previous value.
        /// </summary>
        /// <returns>Color Popped</returns>
        public Color Pop()
        {
            if (_mainColorStack.Count <= 0)
            {
                return Color.white;
            }
            var color = _mainColorStack.Pop();
            GUI.color = _mainColorStack.Peek();
            return color;
        }
        #endregion

        /// <summary>
        /// Gets the current main color
        /// </summary>
        public Color CurrentColor
        {
            get { return _mainColorStack.Peek(); }
        }

        /// <summary>
        /// Gets the current Background color
        /// </summary>
        public Color CurrentBackgroundColor
        {
            get { return _backgroundColorStack.Peek(); }
        }

        /// <summary>
        /// Gets the current Content color
        /// </summary>
        public Color CurrentContentColor
        {
            get { return _contentColorStack.Peek(); }
        }
    }
}