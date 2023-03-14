using System;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Transport;
using MetaPath.Cameras;

namespace MetaPath.Main{
    public class SetCustomCamera : MonoBehaviour
    {
        private CameraManager _cameraManager;
        
        private void OnEnable()
        {
            _cameraManager = new CameraManager(UnityEngine.Camera.main);
            _cameraManager.InjectCustomCameraIntoWrld();

        }
    }
}
