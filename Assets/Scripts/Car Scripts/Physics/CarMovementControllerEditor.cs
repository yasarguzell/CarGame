using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CarMovementController))]
public class CarMovementControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CarMovementController myScript = (CarMovementController)target;
        if(GUILayout.Button("Get Settings From SO"))
        {
            myScript.GetScriptableObjectSettings();
        }
    }
}