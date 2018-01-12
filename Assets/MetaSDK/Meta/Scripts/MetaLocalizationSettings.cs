using UnityEngine;
using System.Collections;
using Meta;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Meta
{
    /// <summary>
    /// To be attached to any gameObject in a Scene containing the meta2 gameObject.
    /// This script allows the user to select from a drop-down menu the localizer to be used.
    /// </summary>
    [Serializable]
    public class MetaLocalizationSettings : MetaBehaviour
    {
        [SerializeField]
        private string _selectedLocalizerName;

        public void Start()
        {
            //Debug.Log("at start: " + m_listIdx); //manual test
            AssignLocalizationType(Type.GetType(_selectedLocalizerName, false));
            ILocalizer localizerMember = GetComponent<ILocalizer>();
            if (localizerMember != null)
            {
                metaContext.Get<MetaLocalization>().SetLocalizer(localizerMember.GetType());
            }
        }

        /// <summary>
        /// Gets a list of all class types that are descendants of the interface 'ILocalizer'
        /// </summary>
        /// <returns>List of Type, which contains all the types descending from ILocalizer </returns>
        public List<Type> GetLocalizationTypes()
        {
            Type baseType = typeof(ILocalizer);
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(baseType.IsAssignableFrom).Where(t => baseType != t).ToList();
            return types;
        }

        /// <summary>
        /// Get the localizer assigned as a component
        /// </summary>
        /// <returns></returns>
        public ILocalizer GetAssignedLocalizer()
        {
            if (Application.isPlaying)
            {
                return metaContext.Get<MetaLocalization>().GetLocalizer();
            }

            var oldComponents = GetComponents<ILocalizer>();
            if (oldComponents != null && oldComponents.Length > 0)
            {
                return oldComponents[0];
            }

            return null;
        }

        /// <summary>
        /// Assigns the localization type as a component member of the GameObject
        /// </summary>
        /// <param name="localizationType"></param>
        public void AssignLocalizationType(Type localizationType)
        {
            if (localizationType == null)
            {
                Debug.LogError("localizationType cannot be null.");
                return;
            }

            if (Application.isPlaying)
            {
                metaContext.Get<MetaLocalization>().SetLocalizer(localizationType);
                //Change the localizer in case that this was not invoked by the editor.
                _selectedLocalizerName = localizationType.ToString();
            }
            else
            {
                //Record a list of existing localizers assigned
                var oldComponents = GetComponents<ILocalizer>();
                if (oldComponents != null && oldComponents.Length > 0 && oldComponents[0].GetType() == localizationType)
                {
                    return; //Avoid reassignment and loss of set script values
                }

                //Assign the new localizer
                gameObject.AddComponent(localizationType);
                
                //Support for hotswapping the localizer method
                if (oldComponents != null)
                {
                    foreach (var component in oldComponents)
                    {
                        if (component != null)
                        {
                            DestroyImmediate((UnityEngine.Object)component);
                        }
                    }
                }
            }
        }
    }
}