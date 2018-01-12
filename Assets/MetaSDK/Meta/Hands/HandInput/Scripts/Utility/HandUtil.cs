using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Meta.HandInput
{
    public class HandUtil
    {
     
        /// <summary>
        /// Instantiates a new HandTemplate prefab.
        /// </summary>
        /// <param name="handData">HandData of the hand to be built. </param>
        /// <returns></returns>
        public static Hand CreateNewHand(HandData handData)
        {
            var prefabName = HandName(handData.HandType);
            var handProxyObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/" + prefabName));
            var handProxy = handProxyObject.GetComponent<Hand>();

            // -- Initialize template hand features.
            handProxy.InitializeHandData(handData);
            return handProxy;
        }

        /// <summary>
        /// Util Method used to build a HandTemplate Prefab
        /// </summary>
        /// <param name="type"> HandType of the hand to be built </param>
        /// <returns></returns>
        public static Hand InitializeTemplateHand(HandType type)
        {
            var prefabName = HandName(type);

            var template = new GameObject(prefabName);
            var templateHand = template.AddComponent<Hand>();

            CreateHandFeature<CenterHandFeature>(templateHand);
            CreateHandFeature<TopHandFeature>(templateHand);
            
            return templateHand;
        }



        public static void SetupCollider(GameObject gameObject)
        {
            if (gameObject.GetComponent<Collider>() != null && !gameObject.GetComponent<Collider>().isTrigger)
            {
                Debug.LogWarning("Setting Collider associated with " + gameObject.name + " to HandFeature layer.This is required to interact with the HandFeature GameObjects in the MetaHands prefab.");
                gameObject.GetComponent<Collider>().isTrigger = true;
            }
        }


        private static T CreateHandFeature<T>(Component handProxy) where T : HandFeature
        {
            var feature = handProxy.gameObject.GetComponentInChildren<T>();
            if (feature == null)
            {
                var featureObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                featureObject.name = typeof(T).Name;
                featureObject.transform.SetParent(handProxy.transform);

                feature = featureObject.AddComponent<T>();
            }

            Switch[typeof(T)](feature.transform);

            return feature;
        }

        private static string HandName(HandType type)
        {
            return string.Format("HandTemplate ({0})", type);
        }

        private static readonly Dictionary<Type, Action<Transform>> Switch = new Dictionary<Type, Action<Transform>> {
            { typeof(CenterHandFeature), transform => { transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);} },
            { typeof(TopHandFeature), transform => { transform.localScale = new Vector3(0.0175f, 0.0175f, 0.0175f);} },
        };
    }
}