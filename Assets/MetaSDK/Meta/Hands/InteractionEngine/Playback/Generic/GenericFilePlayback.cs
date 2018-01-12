using System.IO;
using System.Collections.Generic;

namespace Meta.Internal.Playback
{
    /// <summary>
    /// Handles generic playback functionality for frames in separate files. 
    /// Must be extended for format-specific playback.
    /// </summary>
    /// <typeparam name="T">The type of object to be queued for playback.</typeparam>
    internal class GenericFilePlayback<T> : IPlaybackSource<T>
    {
        protected string _filepath;
        protected List<T> _frames;
        protected T _currFrame;
        protected IFileParser<T> _parser;
        protected int _totalFrames = 0;
        protected int _framesSeen = -1;
        private bool _hasValidSource = false;

        public T CurrentFrame
        {
            get { return _currFrame; }
        }

        public GenericFilePlayback()
        {
            _frames = new List<T>();
        }

        /// <summary>
        /// Creates a new playback object which reads from the given source.
        /// </summary>
        /// <param name="filepath">The path to the playback file.</param>
        public GenericFilePlayback(string filepath) 
        {
            _filepath = filepath;
            if (!ValidRecordedFile(_filepath))
            {
                throw new FileNotFoundException("The playback filepath was not found");
            }
            _hasValidSource = true;
            _frames = new List<T>();
        }

        /// <summary>
        /// Creates a new playback object which reads from the given source.
        /// </summary>
        /// <param name="filepath">The path to the playback directory.</param>
        /// <param name="parser">A predefined parser to be used for file reading.</param>
        public GenericFilePlayback(string filepath, IFileParser<T> parser) : this(filepath)
        {
            _parser = parser;
        }

        #region Playback storage

        /// <summary>
        /// Finds files of the specified format in the playback directory and processes them for playback.
        /// </summary>
        /// <param name="ext"></param>
        public virtual void LoadFrameFiles()
        {
            FileInfo file = new FileInfo(_filepath);
            ProcessFile(file, 0);
            UnityEngine.Debug.Log("Loaded " + _totalFrames + " frames from file.");
        }

        /// <summary>
        /// Reads the file and queues data for this frame using the connected parser.
        /// </summary>
        /// <param name="f">The file to be parsed.</param>
        /// <param name="id">The id for this playback data item.</param>
        protected virtual void ProcessFile(FileInfo f, int id)
        {
            try
            {
                _parser.ParseFileIntoList(f, id, ref _frames);
                _totalFrames = _frames.Count;
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
            }
        }

        /// <summary>
        /// Checks if the path to the frame data is a valid file.
        /// </summary>
        /// <param name="sourcePath">The path of the file to be loaded.</param>
        /// <returns></returns>
        protected bool ValidRecordedFile(string sourcePath)
        {
            return File.Exists(sourcePath);
        }

        #endregion

        #region Playback Controls

        public bool HasValidSource()
        {
            return _hasValidSource;
        }

        public int GetTotalFrameCount()
        {
            return _totalFrames;
        }

        public int GetCurrentFrameIndex()
        {
            return _framesSeen;
        }

        public virtual bool AreFramesLoaded()
        {
            return (_frames.Count != 0) && (_frames.Count == _totalFrames);
        }   

        public virtual bool IsFinished()
        {
            return (_framesSeen + 1 == _totalFrames);
        }

        public virtual bool HasNextFrame()
        {
            return (_framesSeen >= -1 && _framesSeen + 1 < _frames.Count);
        }

        public virtual bool HasPrevFrame()
        {
            return (_framesSeen > 0 && _framesSeen <= _frames.Count);
        }

        public virtual T NextFrame()
        {
            _framesSeen++;
            _currFrame = _frames[_framesSeen];
            return _currFrame;
        }

        public virtual T PreviousFrame()
        {
            _framesSeen--;
            _currFrame = _frames[_framesSeen];
            return _currFrame;
        }

        public string GetPlaybackSourcePath()
        {
            return _filepath;
        }

        public void Reset()
        {
            if (_frames.Count > 0)
            {
                _framesSeen = -1;
                _currFrame = _frames[0];
            }
        }

        public void Clear()
        {
            _frames.Clear();
            _framesSeen = -1;
            _totalFrames = 0;
            _hasValidSource = false;
            _filepath = "";
        }

        public void UseNewPlaybackSourcePath(string filepath, string extension)
        {
            _filepath = filepath;
            if (!ValidRecordedFile(_filepath))
            {
                throw new FileNotFoundException("The playback filepath was not found");
            }
            _frames.Clear();
            _currFrame = default(T);
            _framesSeen = -1;
            LoadFrameFiles();
            _hasValidSource = true;
        }

        #endregion
    }
}