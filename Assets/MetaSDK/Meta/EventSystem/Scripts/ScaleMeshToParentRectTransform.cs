using UnityEngine;
using Meta.Extensions;

namespace Meta
{
    /// <summary>
    /// Scale a mesh to a rect transform
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    public class ScaleMeshToParentRectTransform : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private Vector2 _multiplier = Vector2.one;
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private bool _includeRotation = true;

        private Vector3 _meshSize;
        private RectTransform _parentRectTransform;

        private void Awake()
        {
            _parentRectTransform = transform.parent.GetComponent<RectTransform>();

            SetBounds();
        }

        private void OnEnable()
        {
            UpdateScale();
        }

        private void Update()
        {
            UpdateScale();
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                if (_meshSize == Vector3.zero)
                {
                    if (_includeRotation)
                    {
                        _meshSize = transform.rotation * GetComponent<MeshFilter>().sharedMesh.bounds.size;
                    }
                    else
                    {
                        _meshSize = GetComponent<MeshFilter>().sharedMesh.bounds.size;
                    }
                }

                _parentRectTransform = transform.parent.GetComponent<RectTransform>();
                _meshSize = _meshSize.Abs();

                UpdateScale();
            }
        }

        [ContextMenu("Set Bounds")]
        private void SetBounds()
        {
            Vector3 size = GetComponent<MeshFilter>().sharedMesh.bounds.size;

            if (_includeRotation)
            {
                //Maybe we can use localRotation instead so that we don't have this flag variable -Jared 6/21/2016
                size = transform.rotation * size;
            }

            _meshSize = size.Abs();
        }

        private void UpdateScale()
        {
            //this should be changed to a callback for when the unity canvas is drawn
            float x = (_parentRectTransform.rect.size.x * _multiplier.x) * (1f / _meshSize.x);
            float y = (_parentRectTransform.rect.size.y * _multiplier.y) * (1f / _meshSize.y);
            Vector3 newScale = new Vector3(x, y, 1f);

            if (!newScale.IsNaN())
            {
                transform.localScale = newScale;
            }
        }
    }
}