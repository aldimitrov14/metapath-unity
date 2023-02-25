using System;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Transport;

public class MP_SetCustomRenderCamera : MonoBehaviour
{
    private Camera m_mainCamera;
    
    private void OnEnable()
    {
        m_mainCamera = UnityEngine.Camera.main;
        m_mainCamera.nearClipPlane = 2;
        m_mainCamera.farClipPlane = 8000;


        Api.Instance.CameraApi.SetCustomRenderCamera(m_mainCamera);
    }
}
