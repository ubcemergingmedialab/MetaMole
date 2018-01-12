
namespace Meta.Buttons
{
    /// <summary>
    /// Class that contains the button information of the Meta2
    /// </summary>
    internal class MetaButton : IMetaButton
    {
        private ButtonType _type;
        private ButtonState _state;
        private double _timeStamp;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="type">Type of the button</param>
        /// <param name="state">State of the button</param>
        /// <param name="timestamp">Timestamp when the button was pressed</param>
        public MetaButton(ButtonType type, ButtonState state, double timestamp)
        {
            _type = type;
            _state = state;
            _timeStamp = timestamp;
        }

        /// <summary>
        /// Gets or sets the type of the button
        /// </summary>
        public ButtonType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Gets or sets the current state of the button
        /// </summary>
        public ButtonState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// Gets or sets the time when the button was pressed from start
        /// </summary>
        public double Timestamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }
    }
}
