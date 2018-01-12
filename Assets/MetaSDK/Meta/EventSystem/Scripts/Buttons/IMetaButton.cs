
namespace Meta.Buttons
{
    /// <summary>
    /// Interface to access the Meta2 button information
    /// </summary>
    public interface IMetaButton
    {
        /// <summary>
        /// Gets or sets the type of the button
        /// </summary>
        ButtonType Type
        {
            get;
        }

        /// <summary>
        /// Gets or sets the current state of the button
        /// </summary>
        ButtonState State
        {
            get;
        }

        /// <summary>
        /// Gets or sets the time when the button was pressed from start
        /// </summary>
        double Timestamp
        {
            get;
        }
    }
}
