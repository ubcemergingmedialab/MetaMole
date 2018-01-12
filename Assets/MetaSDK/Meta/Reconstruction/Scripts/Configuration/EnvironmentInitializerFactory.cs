using System;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Creates an <see cref="IEnvironmentInitializer"/> to triggers the environment initialization.
    /// </summary>
    internal class EnvironmentInitializerFactory
    {
        private IMetaContextInternal _metaContext;
        private BaseEnvironmentScanController _environmentScanControllerPrefab;
        private MetaLocalization _metaLocalization;
        private IEnvironmentProfileRepository _environmentProfileRepository;
        private ISlamLocalizer _slamLocalizer;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentInitializerFactory"/> class.
        /// </summary>
        /// <param name="metaContext">The Context to acces to the required services.</param>
        /// <param name="environmentScanControllerPrefab">The scan selector prefab used to control the environment reconstruction.</param>
        internal EnvironmentInitializerFactory(IMetaContextInternal metaContext, BaseEnvironmentScanController environmentScanControllerPrefab)
        {
            if (metaContext == null)
            {
                throw new ArgumentNullException("metaContext");
            }

            _metaContext = metaContext;
            _environmentScanControllerPrefab = environmentScanControllerPrefab;
            _metaLocalization = _metaContext.Get<MetaLocalization>();
            _environmentProfileRepository = _metaContext.Get<IEnvironmentProfileRepository>();
            _slamLocalizer = GameObject.FindObjectOfType<SlamLocalizer>();
        }

        /// <summary>
        /// Creates an <see cref="IEnvironmentInitializer"/> for the given environment selection result.
        /// </summary>
        /// <param name="slamRelocalizationActive">Whether the slam relocalization is active or not.</param>
        /// <param name="surfaceReconstructionActive">Whether the surface reconstruction is active or not.</param>
        /// <param name="environmentProfileType">The environment profile type.</param>
        /// <returns>The environment initializer for the given environment initializer type.</returns>
        internal IEnvironmentInitializer Create(bool slamRelocalizationActive, bool surfaceReconstructionActive, EnvironmentProfileType environmentProfileType)
        {
            if (_slamLocalizer == null)
            {
                return CreateDefaultInitializer();
            }

            if (slamRelocalizationActive)
            {
                return CreateRelocalizationActiveInitializer(surfaceReconstructionActive, environmentProfileType);
            }
            return CreateRelocalizationInactiveInitializer(surfaceReconstructionActive);
        }

        private IEnvironmentInitializer CreateRelocalizationInactiveInitializer(bool surfaceReconstructionActive)
        {
            if (!surfaceReconstructionActive)
            {
                return CreateDefaultInitializer();
            }

            IMetaReconstruction metaReconstruction = _metaContext.Get<IMetaReconstruction>();
            IEnvironmentInitialization initialization = new ReconstructionOnlyEnvironmentInitialization(_slamLocalizer, metaReconstruction, _environmentScanControllerPrefab);
            IEnvironmentReset environmentReset = new ReconstructionOnlyEnvironmentReset(metaReconstruction);
            
            return new EnvironmentInitializer(initialization, environmentReset, _metaLocalization);
        }

        private IEnvironmentInitializer CreateRelocalizationActiveInitializer(bool surfaceReconstructionActive, EnvironmentProfileType environmentProfileType)
        {
            switch (environmentProfileType)
            {
                case EnvironmentProfileType.DefaultProfile:
                    return CreateDefaultProfileInitializer(surfaceReconstructionActive);
                default:
                    throw new Exception(string.Format("EnvironmentProfileType {0} is not supported.", environmentProfileType));
            }
        }

        private IEnvironmentInitializer CreateDefaultProfileInitializer(bool surfaceReconstructionActive)
        {
            ISlamChecker slamChecker = new SlamChecker(_slamLocalizer);
            IEnvironmentProfileSelector environmentProfileSelector = new DefaultEnvironmentProfileSelector(_environmentProfileRepository, slamChecker);
            IEnvironmentInitializationFactory environmentInitializationFactory;
            IEnvironmentReset environmentReset;

            if (surfaceReconstructionActive)
            {
                IMetaReconstruction metaReconstruction = _metaContext.Get<IMetaReconstruction>();
                environmentInitializationFactory = new DefaultEnvironmentWithReconstructionInitializationFactory(_environmentProfileRepository, _slamLocalizer, metaReconstruction, _environmentScanControllerPrefab);
                environmentReset = new DefaultEnvironmentReset(_environmentProfileRepository, metaReconstruction);
            }
            else
            {
                environmentInitializationFactory = new DefaultEnvironmentInitializationFactory(_environmentProfileRepository, _slamLocalizer);
                environmentReset = new DefaultEnvironmentReset(_environmentProfileRepository);
            }
            return new EnvironmentProfileInitializer(environmentProfileSelector, environmentInitializationFactory, environmentReset, _metaLocalization);
        }

        private IEnvironmentInitializer CreateDefaultInitializer()
        {
            return new EnvironmentDefaultInitializer();
        }
    }
}