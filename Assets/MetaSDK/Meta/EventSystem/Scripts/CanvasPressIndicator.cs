using System.Diagnostics;
using UnityEngine;

namespace Meta.UI
{
    public class CanvasPressIndicator : MonoBehaviour
    {
        [SerializeField]
        private Transform _ring;

        [SerializeField]
        private AnimationCurve _scale;

        [SerializeField]
        private AnimationCurve _alpha;

        private float _fullPressIndScale;
        private Stopwatch _pressAnimTimer;
        private RingSegment[] _pressIndSegments;

        private void Start()
        {
            _fullPressIndScale = _ring.transform.localScale.x;
            _pressIndSegments = _ring.GetComponentsInChildren<RingSegment>();
            _pressAnimTimer = new Stopwatch();
        }

        private void Update()
        {
            UpdateAnim();
        }

        public void PlayAnimation(Vector3 position)
        {
            _ring.transform.position = position;

            _pressAnimTimer.Reset();
            _pressAnimTimer.Start();
        }

        private void UpdateAnim()
        {
            _ring.gameObject.SetActive(_pressAnimTimer.IsRunning);

            if (!_pressAnimTimer.IsRunning)
            {
                return;
            }

            float prog = Mathf.Clamp01((float)_pressAnimTimer.Elapsed.TotalSeconds / 0.25f);
            float alpha = _alpha.Evaluate(prog);

            _ring.localScale = Vector3.one *
                (_fullPressIndScale * _scale.Evaluate(prog));

            foreach (RingSegment seg in _pressIndSegments)
            {
                seg.Alpha = alpha;
            }

            if (prog >= 1)
            {
                _pressAnimTimer.Stop();
            }
        }
    }
}