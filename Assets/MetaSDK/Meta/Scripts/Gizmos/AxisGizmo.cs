using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Meta.Gizmo
{
    /// <summary>
    /// Draws the transform axes of a GameObject using gizmos
    /// </summary>
    public class AxisGizmo : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Selected())
                return;

            var length = 0.2f;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.right * length);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.up * length);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.forward * length);
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, length / 25f);

            Handles.color = Color.red;
            Handles.ConeHandleCap(0, transform.position + transform.right * length, Quaternion.LookRotation(transform.right), length / 25f, EventType.Repaint);
            Handles.color = Color.green;
            Handles.ConeHandleCap(0, transform.position + transform.up * length, Quaternion.LookRotation(transform.up), length / 25f, EventType.Repaint);
            Handles.color = Color.blue;
            Handles.ConeHandleCap(0, transform.position + transform.forward * length, Quaternion.LookRotation(transform.forward), length / 25f, EventType.Repaint);
        }

        private bool Selected()
        {
            var currentObject = this.gameObject;
            while (currentObject != null)
            {
                if (Selection.activeObject == currentObject)
                    return true;
                var parent = currentObject.transform.parent;
                if (parent == null)
                    return false;
                currentObject = parent.gameObject;
            }

            return false;
        }
#endif
    }
}