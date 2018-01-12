using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Scale a sprite so that it reaches some target
    /// </summary>
    [ExecuteInEditMode]
    public class ScaleSpriteRendererToTransform : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private Transform _target = null;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private BoxCollider _collider = null;

        private float _length = 0;

        private void Start()
        {
            if (transform.localScale.x == 0)
            {
                transform.localScale = transform.localScale + Vector3.right;
            }

            if (_collider != null)
            {
                _length = _collider.size.x * transform.lossyScale.x;
            }
        }

        private void Update()
        {
            scaleToReachTarget();
        }

        private void scaleToReachTarget()
        {
            if (_target != null && _length > 0)
            {
                //Calculate distance to target
                float distance = Vector3.Distance(_target.transform.position, transform.position);

                //Calculate ratio of distance to length
                float ratio = distance / _length;

                //Set scale
                Vector3 localScale = transform.localScale;
                localScale.x = ratio;
                transform.localScale = localScale;
            }
        }
    }
}