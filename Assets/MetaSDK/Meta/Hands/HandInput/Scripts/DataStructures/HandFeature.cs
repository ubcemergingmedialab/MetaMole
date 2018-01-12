using UnityEngine;

namespace Meta.HandInput
{
    /// <summary>
    /// Represents a piece of the sdk HandData in the world.
    /// </summary>
    public abstract class HandFeature : MetaBehaviour
    {
        /// <summary>
        /// Hand which this feature belongs to.
        /// </summary>
        public Hand Hand { get; protected set; }

        /// <summary>
        /// Has this feature been initialized.
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// Global Hands HandsSettings
        /// </summary>
        protected HandsProvider HandsSettings;

        /// <summary>
        /// HandFeature's Rigibody.
        /// </summary>
        protected Rigidbody Rigidbody;

        /// <summary>
        /// Data structure containing information about the hand.
        /// </summary>
        protected HandData  HandData;

        protected virtual void Start()
        {
            if (!_initialized)
            {
                UnityEngine.Debug.LogWarning("HandFeature has not been initialized.");
            }

            Hand = GetComponentInParent<Hand>();
            HandsSettings = FindObjectOfType<HandsProvider>();

            gameObject.layer = HandsSettings.settings.HandFeatureLayer;

            GetComponent<Collider>().isTrigger = true;

            Rigidbody = gameObject.GetComponent<Rigidbody>();
            if (Rigidbody == null)
            {
                Rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            Rigidbody.useGravity = false;
            Rigidbody.isKinematic = true;
        }

        protected virtual void Update()
        {
            transform.position = Position;
        }

        /// <summary>
        /// This feature's position
        /// </summary>
        public abstract Vector3 Position { get; }

        /// <summary>
        /// Event to get fired when hand leaves the scene
        /// </summary>
        public virtual void OnInvalid() { }

        public void Initialize(HandData  handData)
        {
            HandData = handData;
            _initialized = true;
        }
    }
}
