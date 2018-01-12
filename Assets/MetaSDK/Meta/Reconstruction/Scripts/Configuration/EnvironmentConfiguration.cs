using System;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Entry point of the Environment Initialization process.
    /// </summary>
    internal class EnvironmentConfiguration : MetaBehaviourInternal
    {
        [Tooltip("Prefab of the EnvironmentScanController, used to control the environment scan process.")]
        [HideInInspector]
        [SerializeField]
        private BaseEnvironmentScanController _environmentScanControllerPrefab;

        [Tooltip("Prefab of the MetaReconstruction, used to access to the 3D reconstruction functionalities.")]
        [HideInInspector]
        [SerializeField]
        private Transform _metaReconstructionPrefab;

        [Tooltip("Whether the slam relocalization is active or not.")]
        [HideInInspector]
        [SerializeField]
        private bool _slamRelocalizationActive;

        [Tooltip("Whether the surface reconstruction is active or not.")]
        [HideInInspector]
        [SerializeField]
        private bool _surfaceReconstructionActive;

        [Tooltip("The environment profile strategy.")]
        [HideInInspector]
        [SerializeField]
        private EnvironmentProfileType _environmentProfileType;

        private IEnvironmentInitializer _environmentInitializer;


        internal Transform MetaReconstructionPrefab
        {
            get { return _metaReconstructionPrefab; }
            set { _metaReconstructionPrefab = value; }
        }

        /// <summary>
        /// Whether the surface reconstruction is active or not.
        /// </summary>
        internal bool SurfaceReconstructionActive
        {
            get { return _surfaceReconstructionActive; }
        }

        /// <summary>
        /// Whether the slam relocalization is active or not.
        /// </summary>
        internal bool SlamRelocalizationActive
        {
            get { return _slamRelocalizationActive; }
        }
        
        private void Awake()
        {
            EnvironmentInitializerFactory environmentInitializerFactory = new EnvironmentInitializerFactory(metaContext, _environmentScanControllerPrefab);
            if (_surfaceReconstructionActive)
            {
                new MetaReconstructionFactory(metaContext, _metaReconstructionPrefab).Create(transform.parent);
            }
            _environmentInitializer = environmentInitializerFactory.Create(_slamRelocalizationActive, _surfaceReconstructionActive, _environmentProfileType);
        }

        private void Start()
        {
            if (_environmentInitializer == null)
            {
                throw new NullReferenceException("EnvironmentInitializer");
            }
            
            _environmentInitializer.Start();
        }
    }
}