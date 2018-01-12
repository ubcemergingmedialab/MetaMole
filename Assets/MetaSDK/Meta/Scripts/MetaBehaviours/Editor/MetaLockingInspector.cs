using UnityEngine;
using UnityEditor;

namespace Meta
{

    [CustomEditor(typeof(MetaLocking))]
    [CanEditMultipleObjects]
    public class MetaLockingInspector : Editor
    {

        public override void OnInspectorGUI()
        {
            GUI.changed = false;
            MetaLocking ml = (MetaLocking)target;

            ml.hud = EditorGUILayout.Toggle(new GUIContent("HUD", "Locks to camera space so it sticks to the screen like a HUD."), ml.hud);
            if (ml.hud)
            {
                ml.useDefaultHUDSettings = EditorGUILayout.Toggle("    Default HUD Settings", ml.useDefaultHUDSettings);
                if (!ml.useDefaultHUDSettings)
                {
                    ml.hudLockPosition = EditorGUILayout.Toggle(new GUIContent("    Lock Position", "Locks the position of the object to stay in the HUD"), ml.hudLockPosition);
                    if (ml.hudLockPosition)
                    {
                        ml.hudLockPositionX = EditorGUILayout.Toggle("      X", ml.hudLockPositionX);
                        ml.hudLockPositionY = EditorGUILayout.Toggle("      Y", ml.hudLockPositionY);
                        ml.hudLockPositionZ = EditorGUILayout.Toggle("      Z", ml.hudLockPositionZ);
                    }
                    ml.hudLockRotation = EditorGUILayout.Toggle(new GUIContent("    Lock Rotation", "Locks the rotation of the object to stay in the HUD"), ml.hudLockRotation);
                    if (ml.hudLockRotation)
                    {
                        ml.hudLockRotationX = EditorGUILayout.Toggle("      X", ml.hudLockRotationX);
                        ml.hudLockRotationY = EditorGUILayout.Toggle("      Y", ml.hudLockRotationY);
                        ml.hudLockRotationZ = EditorGUILayout.Toggle("      Z", ml.hudLockRotationZ);
                    }
                }
            }
            ml.orbital = EditorGUILayout.Toggle(new GUIContent("Orbital", "Locks to orbital so that it is locked to your arm length away from you and looks at you"), ml.orbital);
            if (ml.orbital)
            {
                ml.useDefaultOrbitalSettings = EditorGUILayout.Toggle("    Default Orbital Settings", ml.useDefaultOrbitalSettings);
                if (!ml.useDefaultOrbitalSettings)
                {
                    GUILayout.BeginHorizontal();
                    ml.orbitalLockDistance = EditorGUILayout.Toggle("    Lock Distance", ml.orbitalLockDistance);
                    if (ml.orbitalLockDistance)
                    {
                        //ml.userReachDistance = EditorGUILayout.Toggle("        User Reach Distance", ml.userReachDistance);
                        //if (!ml.userReachDistance)
                        //{
                        ml.lockDistance = EditorGUILayout.FloatField(ml.lockDistance);
                        //}
                    }
                    GUILayout.EndHorizontal();
                    ml.orbitalLookAtCamera = EditorGUILayout.Toggle("    Look At Camera", ml.orbitalLookAtCamera);
                    if (ml.orbitalLookAtCamera)
                    {
                        ml.orbitalLookAtCameraFlipY = EditorGUILayout.Toggle("      Flip Y", ml.orbitalLookAtCameraFlipY);
                    }
                }
            }

            if (GUI.changed)
            {
                foreach (Object t in targets)
                {
                    MetaLocking metaLocking = (MetaLocking)t;

                    metaLocking.hud = ml.hud;
                    metaLocking.useDefaultHUDSettings = ml.useDefaultHUDSettings;
                    metaLocking.hudLockPosition = ml.hudLockPosition;
                    metaLocking.hudLockPositionX = ml.hudLockPositionX;
                    metaLocking.hudLockPositionY = ml.hudLockPositionY;
                    metaLocking.hudLockPositionZ = ml.hudLockPositionZ;
                    metaLocking.hudLockRotation = ml.hudLockRotation;
                    metaLocking.hudLockRotationX = ml.hudLockRotationX;
                    metaLocking.hudLockRotationY = ml.hudLockRotationY;
                    metaLocking.hudLockRotationZ = ml.hudLockRotationZ;

                    metaLocking.orbital = ml.orbital;
                    metaLocking.useDefaultOrbitalSettings = ml.useDefaultOrbitalSettings;
                    metaLocking.orbitalLockDistance = ml.orbitalLockDistance;
                    //metaLocking.userReachDistance = ml.userReachDistance;
                    metaLocking.lockDistance = ml.lockDistance;
                    metaLocking.orbitalLookAtCamera = ml.orbitalLookAtCamera;
                    metaLocking.orbitalLookAtCameraFlipY = ml.orbitalLookAtCameraFlipY;
                    EditorUtility.SetDirty(metaLocking);
                }
            }
        }

    }
}