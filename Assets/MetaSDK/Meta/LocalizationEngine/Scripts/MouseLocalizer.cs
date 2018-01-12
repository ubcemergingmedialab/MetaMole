using UnityEngine;

namespace Meta
{
    public class MouseLocalizer : MetaBehaviour, ILocalizer
    {
        [SerializeField]
        private bool _invertVerticalMovement = false;

        [SerializeField]
        private float _sensitivity = 0.5f;

        private float _deltaX;
        private float _deltaY;
        private bool _previouslyLocked;
        private GameObject _targetGO;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                _sensitivity *= Input.GetAxis("Mouse ScrollWheel") * .1f + 1f;
                int direction = _invertVerticalMovement ? 1 : -1;

                //Update if the cursor is locked or confined
                if (Cursor.lockState != CursorLockMode.None)
                {
                    _deltaX = Input.GetAxis("Mouse X") * _sensitivity;
                    _deltaY = Input.GetAxis("Mouse Y") * _sensitivity * direction;
                }
            }

            //Handle grab/releasing of mouse
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _previouslyLocked = Cursor.lockState == CursorLockMode.Locked;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetKeyUp(KeyCode.Mouse1) && !_previouslyLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void SetTargetGameObject(GameObject targetGO)
        {
            _targetGO = targetGO;
        }

        public void ResetLocalizer()
        {
            if (_targetGO != null)
            {
                _targetGO.transform.localRotation = Quaternion.identity;
            }
        }

        public void UpdateLocalizer()
        {
            if (_targetGO != null)
            {
                Vector3 rotEuler = _targetGO.transform.localRotation.eulerAngles;
                _targetGO.transform.localRotation = Quaternion.Euler(rotEuler.x + _deltaY, rotEuler.y + _deltaX, rotEuler.z);
                _deltaX = _deltaY = 0f; //Once input has been used, it shouldn't be used again
            }
        }
    }
}