using Meta.HandInput;

namespace Meta
{
    /// <summary>   Interface for hand data source. </summary>
    public interface IHandDataSource
    {
        /// <summary>   Initialises the hand data source. </summary>
        void InitHandDataSource();

    
        /// <summary>   Gets hand data. </summary>
        /// <param name="leftHandData">     Information describing the left hand. </param>
        /// <param name="rightHandData">    Information describing the right hand. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
    
        bool GetHandDataFromSensor(ref HandData leftHandData, ref HandData rightHandData);

    
        /// <summary>   Sets hand options. </summary>
        /// <param name="handOptions">  Options for controlling the hand. </param>
    
        void SetHandOptions(HandProcessorOptions handOptions);
    }
}
