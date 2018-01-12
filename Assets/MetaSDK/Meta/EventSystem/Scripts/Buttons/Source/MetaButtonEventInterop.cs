using System.Runtime.InteropServices;

namespace Meta.Buttons
{
    /// <summary>
    /// Interop class for getting Button Events
    /// </summary>
    internal class MetaButtonEventInterop : IMetaButtonEventInterop
    {
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "getNextButtonState")]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool GetNextButtonState(out int opType, out int opState, out double oTimestamp);

        /// <summary>
        /// Get a button event if available
        /// </summary>
        /// <returns>Button Event, null if there is no event</returns>
        public IMetaButton GetButtonEvent()
        {
            int buttonType;
            int buttonState;
            double timestamp;
            bool result = GetNextButtonState(out buttonType, out buttonState, out timestamp);
            if (!result)
            {
                return null;
            }
            if (buttonType == 0x08)
            {
                return null;
            }

            return new MetaButton((ButtonType)buttonType, (ButtonState)buttonState, timestamp);
        }
    }
}
