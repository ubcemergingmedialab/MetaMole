
namespace Meta.Buttons
{
    /// <summary>
    /// Enumerate the possible states of a button
    /// </summary>
    public enum ButtonState
    {
        /// <summary>
        /// Release or idle
        /// </summary>
        ButtonRelease = 0x00,

        /// <summary>
        /// Short press
        /// </summary>
        ButtonShortPress = 0x01,

        /// <summary>
        /// Long press
        /// </summary>
        ButtonLongPress = 0x03,
    }
}
