using System;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Creates an <see cref="IMetaReconstruction"/>.
    /// </summary>
    internal class MetaReconstructionFactory
    {
        private IMetaContextInternal _metaContext;
        private Transform _metaReconstructionPrefab;

        /// <summary>
        /// Creates an instance of <see cref="MetaReconstructionFactory"/> class.
        /// </summary>
        /// <param name="metaContext">The Context to acces to the required services.</param>
        /// <param name="environmentScanControllerPrefab">The prefab with the meta reconstruction.</param>
        internal MetaReconstructionFactory(IMetaContextInternal metaContext, Transform metaReconstructionPrefab)
        {
            if (metaContext == null)
            {
                throw new ArgumentNullException("metaContext");
            }

            if (metaReconstructionPrefab == null)
            {
                throw new ArgumentNullException("metaReconstructionPrefab");
            }

            _metaContext = metaContext;
            _metaReconstructionPrefab = metaReconstructionPrefab;
        }

        /// <summary>
        /// Creates an <see cref="IEnvironmentInitializer"/> for the given environment selection result.
        /// </summary>
        /// <param name="environmentInitializaterType">The environment initializer type.</param>
        /// <returns>The environment initializer for the given environment initializer type.</returns>
        public IMetaReconstruction Create(Transform parent)
        {
            Transform metaReconstructionTransform = MonoBehaviour.Instantiate(_metaReconstructionPrefab);
            metaReconstructionTransform.parent = parent;
            metaReconstructionTransform.position = Vector3.zero;

            IMetaReconstruction metaReconstruction = metaReconstructionTransform.GetComponent<IMetaReconstruction>();

            if (metaReconstruction != null)
            {
                if (!_metaContext.ContainsModule<IMetaReconstruction>())
                {
                    _metaContext.Add(metaReconstruction);
                }
                _metaContext.Add<IMeshGenerator>(new MeshGenerator(true, EnvironmentConstants.MaxTriangles));
                _metaContext.Add<IModelFileManipulator>(new OBJFileManipulator());
            }

            return metaReconstruction;
        }
    }
}