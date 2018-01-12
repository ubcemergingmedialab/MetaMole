using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Handles the loading and application of alignment data.
    /// This module observes the alignment-directory-file and 
    /// is alerted when updates occur to it (i.e when the 'active'
    /// profile changes). 
    /// </summary>
    internal class AlignmentHandler : IEventReceiver
    {
        /// <summary>
        /// The Alignment update listeners. These will receive events when the alignment is updated.
        /// </summary>
        public readonly List<IAlignmentUpdateListener> AlignmentUpdateListeners = new List<IAlignmentUpdateListener>();

        /// <summary>
        /// A reference to the main metaContext instance
        /// </summary>
        private IMetaContextInternal _metaContext;

        /// <summary>
        /// The path to the alignment-directory-file's directory. 
        /// </summary>
        private readonly string _alignmentDirFilePathDir = string.Format(@"{0}\UserSettings\", Environment.GetEnvironmentVariable("META_ROOT"));

        private readonly string _alignmentDirFileName = "meta_calibration.cdb";

        /// <summary>
        /// The path to the alignment-directory-file. 
        /// </summary>
        private string _fullADFPath
        {
            get { return (_alignmentDirFilePathDir + _alignmentDirFileName); }
        }

        private FileSystemWatcher _fsWatcher;

        /// <summary>
        /// The index of the alignment profile that is active (retrieved from the alignment-directory-file)
        /// </summary>
        private int _alignmentIndexFromFile = -1;

        /// <summary>
        /// The index of the alignment profile that is active (retrieved from the alignment-directory-file)
        /// </summary>
        public int AlignmentProfileIndex
        {
            get { return _alignmentIndexFromFile; }
        }

        /// <summary>
        /// Set up the file system watcher, add to the delegate 'changed' an anonymous function which
        /// loads the updates and updates AlignmentUpdateListeners.
        /// </summary>
        /// <param name="eventHandlers"></param>
        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnStart(OnStart);

            //Allow for the delegates to be deleted.
            eventHandlers.SubscribeOnApplicationQuit(OnApplicationQuit);
        }

        private void OnStart()
        {
            if (File.Exists(_fullADFPath))
            {
                _fsWatcher = new FileSystemWatcher(_alignmentDirFilePathDir);
                _fsWatcher.Filter = _alignmentDirFileName;
                _fsWatcher.NotifyFilter = NotifyFilters.LastWrite;
                _fsWatcher.EnableRaisingEvents = true;
                _fsWatcher.Changed += (object o, FileSystemEventArgs e) =>
                {
                    var profile = _updateAlignmentProfile();
                    foreach (IAlignmentUpdateListener listener in AlignmentUpdateListeners)
                    {
                        listener.OnAlignmentUpdate(profile);
                    }
                };
            }

            //Run this at least once so that the index is loaded from the alignment-directory-file.
            var bridge = GameObject.FindObjectOfType<MetaContextBridge>();
            if (bridge != null)
            {
                _metaContext = bridge.CurrentContextInternal;
            }
            _updateAlignmentProfile();
        }

        private void OnApplicationQuit()
        {
            if (_fsWatcher != null)
            {
                _fsWatcher.Dispose();
            }
            _fsWatcher = null;
        }

        /// <summary>
        /// Updates the alignment profile, if necessary.
        /// </summary>
        private AlignmentProfile _updateAlignmentProfile()
        {
            var oldAlignmentIndex = _alignmentIndexFromFile;
            _alignmentIndexFromFile = _getActiveAlignmentIndexFromFile();

            if (_alignmentIndexFromFile != oldAlignmentIndex
                && _metaContext != null
                && _metaContext.ContainsModule<IUserSettings>() //Get the more permissive user settings
                && _metaContext.Get<IUserSettings>().HasKey(MetaConfiguration.AlignmentProfile, _alignmentIndexFromFile)) //Check that the index from file correctly indexes an alignment profile
            {
                var newProfile =
                    _metaContext.Get<IUserSettings>()
                        .GetSetting<AlignmentProfile>(MetaConfiguration.AlignmentProfile, _alignmentIndexFromFile);

                //May intentionally overwrite the existing profile
                _metaContext.Add<AlignmentProfile>(newProfile);
                return newProfile;
            }
            return null;
        }

        /// <summary>
        /// Loads the active alignment profile index from the alignment-directory-file. 
        /// </summary>
        private int _getActiveAlignmentIndexFromFile()
        {
            string[] lines = null;
            if (File.Exists(_fullADFPath))
            {
                lines = File.ReadAllLines(_fullADFPath);
            }
            else
            {
                return -1;
            }

            // Check if the version of the file is the last one.
            if (lines.Length == 0 || lines[0] != AlignmentFileFormat.CurrentVersion)
            {
                return -1;
            }

            //Get a single profile line count
            int fileSingleProfileLines = AlignmentFileFormat.GetSingleProfileLines();
            //Calculate the min lines the file should have. This is one profile, plus the header line count.
            int fileMinLines = fileSingleProfileLines + AlignmentFileFormat.HeaderLines;

            //Just enough for header, and one single profile across lines.
            if (lines != null && lines.Length >= fileMinLines)
            {
                //Get the index to the llast profile in the file
                int lastProfileIndex = lines.Length - fileSingleProfileLines;
                int lastNameLine = lastProfileIndex + AlignmentFileFormat.GetIndexInProfile(AlignmentFileFormat.FieldType.Name);

                //The name of the last profile should be empty signifying that it is the 'active' profile.
                if (lines[lastNameLine].Equals(""))
                {
                    try
                    {
                        int lastIndexLine = lastProfileIndex + AlignmentFileFormat.GetIndexInProfile(AlignmentFileFormat.FieldType.Index);
                        //The index from the last profile
                        return Int32.Parse(lines[lastIndexLine]);
                    }
                    catch (Exception)
                    { }
                }
            }

            return -1;
        }
    }

}
