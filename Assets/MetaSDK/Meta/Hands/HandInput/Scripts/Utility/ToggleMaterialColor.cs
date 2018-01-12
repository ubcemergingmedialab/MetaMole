using UnityEngine;

namespace Meta.Utility
{
    public class ToggleMaterialColor : MonoBehaviour
    {
        public string PropertyName = "_Color";
        private float _interpAmount;
        private float _targetInterpAmount;
        private float _interpAmountVelocity;
        private Material _material;

        [ColorUsage(false, true, 0f, 5f, 0f, 5f)]
        public Color Active = new Color(1f, 2f, 3f);
        [ColorUsage(false, true, 0f, 5f, 0f, 5f)]
        public Color Idle = new Color(0.75f, 0.75f, 0.75f);


        void Awake()
        {
            _material = Instantiate(GetComponent<Renderer>().material);
            GetComponent<Renderer>().material = _material;
        }

        void Update()
        {
            _interpAmount = Mathf.SmoothDamp(_interpAmount, _targetInterpAmount, ref _interpAmountVelocity, .2f);
            _material.SetColor(PropertyName, Color.Lerp(Idle, Active, _interpAmount));
        }

        public void ToggleColor(bool active)
        {
            _targetInterpAmount = active ? 1f : 0f;
        }

    }
}