using UnityEngine;

namespace Meta
{
    /// <summary>
    /// This may be attached to any game object in order to maintain transform information 
    /// of the game object after Unity is no longer playing. The transform information may
    /// then be recovered for a different session.
    /// </summary>
    public class GameObjectUserSettings : MetaBehaviour
    {
        /// <summary>
        /// The sequence of characters used to divide a path
        /// </summary>
        private string _divider = "_";

        [SerializeField]
        private string _keyOverride;

        private string _targetObjectPath;

        /// <summary>
        /// Whether to load transform settings
        /// </summary>
        public bool LoadSettings = true;

        /// <summary>
        /// Whether to save transform settings
        /// </summary>
        public bool SaveSettings = true;

        /// <summary>
        /// The key used to save rotation data
        /// </summary>
        public string m_rotKey
        {
            get { return _targetObjectPath + _divider + "rot"; }
        }

        /// <summary>
        /// The key used to save position data
        /// </summary>
        public string m_posKey
        {
            get { return _targetObjectPath + _divider + "pos"; }
        }

        /// <summary>
        /// The key used to save scale data
        /// </summary>
        public string m_scaleKey
        {
            get { return _targetObjectPath + _divider + "scl"; }
        }

        public void OnApplicationQuit()
        {
            SaveObjectSettings(); 
        }

        // Use this for initialization
        void Start()
        {
            _targetObjectPath = _keyOverride.Length>0? _keyOverride : GetGameObjectPath(gameObject);
            LoadObjectSettings();
        }
 
        /// <summary>
        /// Saves the transform of the object to the position,rotation and scale keys.
        /// </summary>
        void SaveObjectSettings()
        {
            if (SaveSettings)
            {
                var settings = metaContext.GetUserSettings();
                settings.SetSetting(m_posKey, transform.localPosition);
                settings.SetSetting(m_rotKey, transform.localRotation);
                settings.SetSetting(m_scaleKey, transform.localScale);
            }
        }

        /// <summary>
        /// Loads position, rotation and scale data into the transform of the gameobject.
        /// </summary>
        void LoadObjectSettings()
        {
            if (LoadSettings)
            { 
                var settings = metaContext.GetUserSettings(); 
                if (settings.HasKey(m_posKey))
                {
                    transform.localPosition = settings.GetSetting<Vector3>(m_posKey);
                }
                if (settings.HasKey(m_rotKey))
                {
                    transform.localRotation = settings.GetSetting<Quaternion>(m_rotKey);
                }
                if (settings.HasKey(m_scaleKey))
                {
                    transform.localScale = settings.GetSetting<Vector3>(m_scaleKey);
                }
            }
        }

        /// <summary>
        /// Gets the path of a gameobject in the scene relative to the root level.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetGameObjectPath(GameObject obj)
        {
            string path = "/" + obj.name;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }
            return path;
        }
    }
}