using System;
using UnityEngine;
using System.IO;

namespace Meta
{
    /// <summary>
    /// Assigns maps to two materials- one for each eye.
    /// </summary>
    public class AssignMaps : MetaBehaviour, IAlignmentUpdateListener
    {

        private enum LoadState
        {
            NotLoaded=0,
            HasLoadedFinal,
            LoadFailed,
            HasLoadedProxy
        }

        /// <summary>
        /// The current state of loading the textures
        /// </summary>
        private LoadState _loadState = LoadState.NotLoaded;

        private string _MapsDir = string.Format(@"{0}\Maps\", Environment.GetEnvironmentVariable("META_ROOT"));

        /// <summary>
        /// Fallback Mask maps
        /// </summary>
        [SerializeField]
        private Texture2D[] _fallbackMaskMaps;

        /// <summary>
        /// Fallback Distortion maps
        /// </summary>
        [SerializeField]
        private Texture2D[] _fallbackDistortionMaps;

        /// <summary>
        /// Alignment textures loaded from file. 
        /// First two are mask textures, Left and Right respective,
        /// Next two are distortion textures, Left and Right respective.
        /// </summary>
        private readonly Texture2D[] _fileAlignmentTextures = new Texture2D[4];

        /// <summary>
        /// The mask maps to be used for rendering
        /// </summary>
        [SerializeField]
        private readonly Texture2D[] _activeMaskMaps = new Texture2D[2];

        /// <summary>
        /// The distortion maps to be used for rendering
        /// </summary>
        [SerializeField]
        private readonly Texture2D[] _activeDistortionMaps = new Texture2D[2];

        private void Start()
        {
            //Register this 
            if (metaContext != null && metaContext.ContainsModule<AlignmentHandler>())
            {
                AlignmentHandler handler = metaContext.Get<AlignmentHandler>();
                handler.AlignmentUpdateListeners.Add(this);
            }
        }

        public void AssignTo(Material matLeft, Material matRight)
        {
            if (_loadState == LoadState.NotLoaded || _loadState == LoadState.HasLoadedProxy)
            {
                LoadMaps();
                matLeft.SetTexture("_DistMap", _activeDistortionMaps[0]);
                matRight.SetTexture("_DistMap", _activeDistortionMaps[1]);
                matLeft.SetTexture("_Mask", _activeMaskMaps[0]);
                matRight.SetTexture("_Mask", _activeMaskMaps[1]);
            }
        }

        /// <summary>
        /// Attempt to load maps from the alignment profile retrieved from the UserSettings
        /// fallback maps are used in the event that the maps cannot be loaded.
        /// The '_loadState' member of this instance is modified accordingly.
        /// </summary>
        public void LoadMaps()
        {
            AlignmentProfile profile = null;

            if (metaContext != null && metaContext.ContainsModule<AlignmentProfile>())
            {
                profile = metaContext.Get<AlignmentProfile>();
            }

            if (profile != null && profile.ProfileMapPathsValid(_MapsDir))
            {

                string[] filenames = new[]
                {
                    profile.MaskMapPathLeft,
                    profile.MaskMapPathRight,
                    profile.DistortionMapPathLeft,
                    profile.DistortionMapPathRight
                };

                for (int i = 0; i < 4; ++i)
                {
                    var fileData = File.ReadAllBytes(_MapsDir + filenames[i]);
                    //Important to get the right filtering

                    if (!_fileAlignmentTextures[i])
                    {
                        _fileAlignmentTextures[i] = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                    }

                    _fileAlignmentTextures[i].hideFlags = HideFlags.HideAndDontSave;
                    _fileAlignmentTextures[i].filterMode = FilterMode.Point;
                    _fileAlignmentTextures[i].wrapMode = TextureWrapMode.Clamp;
                    _fileAlignmentTextures[i].LoadImage(fileData); //..this will auto-resize the texture dimensions.
                }

                //Assign the file alignment textures to the correct variables
                _activeMaskMaps[0] = _fileAlignmentTextures[0];
                _activeMaskMaps[1] = _fileAlignmentTextures[1];
                _activeDistortionMaps[0] = _fileAlignmentTextures[2];
                _activeDistortionMaps[1] = _fileAlignmentTextures[3];
                _loadState = LoadState.HasLoadedFinal;
            }
            else
            {
                for (int i = 0; i < 2; ++i)
                {
                    _activeDistortionMaps[i] = _fallbackDistortionMaps[i];
                    _activeMaskMaps[i] = _fallbackMaskMaps[i];
                }

                //The purpose of the following is to put this instance into a state where it may
                // attempt to load maps again.
                //The user-aligned maps may not be available at this time because Unity is currently not playing. 
                //Therefor the fallback maps may be used in place of them.
                _loadState = (metaContext == null) ? LoadState.HasLoadedProxy : LoadState.LoadFailed;
            }
        }

        public void OnAlignmentUpdate(AlignmentProfile newProfile)
        {
            //Reset the load-state to load the new maps
            _loadState = LoadState.NotLoaded;
        }
    }
}