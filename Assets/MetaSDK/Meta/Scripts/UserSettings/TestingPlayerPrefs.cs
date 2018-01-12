using UnityEngine;
using System.Collections;
using Meta;

namespace Meta
{
    public class TestingPlayerPrefs : MetaBehaviour
    {


        public struct Workspace
        {
            public int UniqueID;
            // ...
            public Layout layout;

        };

        public struct Layout
        {
            //public Frame f;
        };
        // Use this for initialization
        void Start()
        {
            //PlayerPrefs.SetString("Hey", "Hello World");
            //PlayerPrefs.Save();

            bool flag = false;

            metaContext.Get<UserSettings>().UserLogin(new Credentials("Meta", "Company"));

            if (flag)
            {
                metaContext.Get<UserSettings>().SetSetting("here be floats", 1.26f);
                metaContext.Get<UserSettings>().SetSetting(MetaConfiguration.Workspace, 0, "UniqueID=0;....");
            }
            else
            {
                metaContext.Get<UserSettings>().DeserializePersistentSettings();
            }
            //metaContext.Get<UserSettings>().DeserializeFromDisk();

            var val = metaContext.Get<UserSettings>().GetSetting<float>("here be floats");
            var val2 = metaContext.Get<UserSettings>().GetSetting<float>(MetaConfiguration.Workspace, 0);

            Debug.Log("Here is the float: " + val + ", here is another: " + val2);
            metaContext.Get<UserSettings>().SerializePersistentSettings();
            //PlayerPrefs.SetFloat("hi", 200f);
            //PlayerPrefs.SetInt("hi", 1);
            //PlayerPrefs.Save();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("Prefs: " + PlayerPrefs.GetFloat("hi"));
            //Debug.Log("String: " + PlayerPrefs.GetInt("Hey", 0));
            // The test below 

        }
    }

}