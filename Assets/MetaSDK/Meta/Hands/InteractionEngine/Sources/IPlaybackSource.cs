namespace Meta.Internal.Playback
{
    /// <summary>
    /// Interface for data playback classes.
    /// Provides controls for playback functionality.
    /// </summary>
    /// <typeparam name="T">The type of frame data.</typeparam>
    internal interface IPlaybackSource<T>
    {

        /// <summary>
        /// Whether playback of the loaded recorded has been completed.
        /// </summary>
        /// <returns></returns>
        bool IsFinished();

        /// <summary>
        /// Whether the source for this playback source is valid.
        /// </summary>
        /// <returns></returns>
        bool HasValidSource();

        /// <summary>
        /// Loads valid frame files from the directory into memory for playback.
        /// </summary>
        /// 
        void LoadFrameFiles();

        /// <summary>
        /// Gets the total number of loaded frames.
        /// </summary>
        /// <returns>The number of loaded frames.</returns>
        int GetTotalFrameCount();

        /// <summary>
        /// Gets the index of the current frame in the playback queue.
        /// </summary>
        /// <returns>The index of the current frame in the playback playlist.</returns>
        int GetCurrentFrameIndex();

        /// <summary>
        /// Indicates if the directory has been fully loaded and is ready for playback.
        /// </summary>
        /// <returns>True, if all files are parsed and loaded into memory. Else, false.</returns>
        bool AreFramesLoaded();

        /// <summary>
        /// Retrieves the current frame being played back.
        /// </summary>
        /// <returns></returns>
        T CurrentFrame { get; }

        /// <summary>
        /// Retrieves the next frame for playback.
        /// </summary>
        /// <returns>The frame object following the current frame index.</returns>
        T NextFrame();

        /// <summary>
        /// Retrieves the previous frame for playback.
        /// </summary>
        /// <returns>The frame object type preceding the current frame index.</returns>
        T PreviousFrame();

        /// <summary>
        /// Checks if there is another frame to be played after the current frame.
        /// </summary>
        /// <returns>True, if another frame exists after the index of the current frame. Else, false.</returns>
        bool HasNextFrame();

        /// <summary>
        /// Checks if there is another frame before the current frame.
        /// </summary>
        /// <returns>True, if another frame exists before the index of the current frame. Else, false.</returns>
        bool HasPrevFrame();

        /// <summary>
        /// Returns the path of the playback directory.
        /// </summary>
        /// <returns>String of the path being used for playback.</returns>
        string GetPlaybackSourcePath();

        /// <summary>
        /// Resets the playback to the first frame of the loaded playback session.
        /// </summary>
        void Reset();

        /// <summary>
        /// Clears any data currently stored about a loaded recording.
        /// </summary>
        void Clear();

        /// <summary>
        /// Changes the source for this playback source.
        /// </summary>
        void UseNewPlaybackSourcePath(string path, string extension);
    }
}