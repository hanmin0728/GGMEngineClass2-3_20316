using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(NewTopDownCamera))]
public class CameraWithEditor : Editor
{
    private NewTopDownCamera newTopDownCamera;

    public override void OnInspectorGUI()
    {
        newTopDownCamera = (NewTopDownCamera)target;
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (!newTopDownCamera || !newTopDownCamera.camTarget)
        {
            return;
        }

        Transform camTarget = newTopDownCamera.camTarget.transform;
        Vector3 posTarget = camTarget.position;
        posTarget.y += newTopDownCamera.targetHeight;

        Handles.color = Color.red;
        Handles.DrawSolidDisc(posTarget, Vector3.up, newTopDownCamera.camDistance);

        Handles.color = Color.green;
        Handles.DrawSolidDisc(posTarget, Vector3.up, newTopDownCamera.camDistance);

        Handles.color = Color.blue;
        newTopDownCamera.camDistance = Handles.ScaleSlider(
            newTopDownCamera.camDistance,
            posTarget,
            -camTarget.forward,
            Quaternion.identity,
            newTopDownCamera.camDistance,
            0.1f);
        newTopDownCamera.camDistance = Mathf.Clamp(newTopDownCamera.camDistance, 2f, float.MaxValue);

        Handles.color = Color.yellow;
        newTopDownCamera.camHeight = Handles.ScaleSlider(
            newTopDownCamera.camHeight,
            posTarget,
            Vector3.up,
            Quaternion.identity,
            newTopDownCamera.camHeight,
            0.1f);
        newTopDownCamera.camHeight = Mathf.Clamp(newTopDownCamera.camHeight, 2f, float.MaxValue);

        GUIStyle guiStyleLabel = new GUIStyle();
        guiStyleLabel.fontSize = 15;
        guiStyleLabel.normal.textColor = Color.white;

        guiStyleLabel.alignment = TextAnchor.UpperCenter;
        Handles.Label(posTarget + (-camTarget.forward * newTopDownCamera.camDistance), "DISCTANCE", guiStyleLabel);
        guiStyleLabel.alignment = TextAnchor.MiddleLeft;
        Handles.Label(posTarget + (Vector3.up * newTopDownCamera.camDistance), "HEIGHT", guiStyleLabel);

        newTopDownCamera.TopDownFollowCamera();
    }
}