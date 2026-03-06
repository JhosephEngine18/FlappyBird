using System;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DebugCameraMode : MonoBehaviour
{
    [SerializeField] private Transform camera;
    void LateUpdate()
    {
        camera.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
    }
}
