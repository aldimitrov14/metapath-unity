using System.Collections;
using Wrld;
using Wrld.Space;
using UnityEngine;

public class SetCustomRenderCamera : MonoBehaviour
{
    private Camera m_mainCamera;
    public float speed = 10.0f;
    private double latitudeDegrees = 37.7858;
    private double longitudeDegrees = -122.401;
    private double distanceFromInterest = 15.0;
    
    private void OnEnable()
    {
        m_mainCamera = UnityEngine.Camera.main;
        m_mainCamera.nearClipPlane = 2;
        m_mainCamera.farClipPlane = 8000;
        Api.Instance.CameraApi.SetCustomRenderCamera(m_mainCamera);
        Api.Instance.CameraApi.MoveTo(LatLong.FromDegrees(latitudeDegrees, longitudeDegrees), distanceFromInterest);
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_mainCamera.transform.position = m_mainCamera.transform.position + new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
    }

}




