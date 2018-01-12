using System;
using System.Collections;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Controls the transition between surface reconstruction materials.
    /// </summary>
    public class ReconstructionTransition : MonoBehaviour
    {
        [Tooltip("Object that manages the environment reconstruction.")]
        [SerializeField]
        private MetaReconstruction _metaReconstruction;

        [Tooltip("Duration of the transition in seconds.")]
        [SerializeField]
        private float _transitionDuration = 5f;

        private void Start()
        {
            _metaReconstruction.ReconstructionPaused.AddListener(ScanMeshTransition);
            _metaReconstruction.ReconstructionLoaded.AddListener(LoadMeshTransition);
        }

        private void ScanMeshTransition()
        {
            StartCoroutine(MeshTransition(_metaReconstruction.ReconstructionRoot, ReplaceScannedMeshMaterial));
        }

        private void LoadMeshTransition(GameObject reconstruction)
        {
            StartCoroutine(MeshTransition(reconstruction, ReplaceLoadedMeshMaterial));
        }

        private void ReplaceScannedMeshMaterial()
        {
            _metaReconstruction.ChangeReconstructionMaterial(_metaReconstruction.OcclusionMaterial);
        }

        private void ReplaceLoadedMeshMaterial()
        {
            _metaReconstruction.ChangeLoadedReconstructionMaterial(_metaReconstruction.OcclusionMaterial);
        }

        private IEnumerator MeshTransition(GameObject reconstruction, Action action)
        {
            MeshRenderer[] meshes = reconstruction.GetComponentsInChildren<MeshRenderer>();
            if (meshes.Length > 0)
            {
                Color initialNearColor = meshes[0].material.GetColor("_Color");
                Color initialFarColor = meshes[0].material.GetColor("_FarColor");
                float initialTime = Time.time;

                while (Time.time - initialTime < _transitionDuration)
                {
                    foreach (MeshRenderer meshRenderer in meshes)
                    {
                        // if the mesh was destroyed, just stop the transition.
                        if (meshRenderer == null)
                        {
                            yield break;
                        }
                        meshRenderer.material.SetColor("_Color", Color.Lerp(initialNearColor, Color.black, (Time.time - initialTime) / _transitionDuration));
                        meshRenderer.material.SetColor("_FarColor", Color.Lerp(initialFarColor, Color.black, (Time.time - initialTime) / _transitionDuration));
                    }
                    yield return null;
                }
            }
            action.Invoke();
        }
    }
}