using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Transport;

namespace MetaPath.Cameras{
    public class CameraManager
    {
        private Camera _mainCamera;

        public Camera MainCamera{
            get { return _mainCamera; }
            set { _mainCamera = value; }
        }

        public CameraManager(Camera camera){
            _mainCamera = camera;
        }

        public void InjectCustomCameraIntoWrld(){

            SetCameraClipPlane();

            Api.Instance.CameraApi.SetCustomRenderCamera(_mainCamera);
        }

        private void SetCameraClipPlane(){
            _mainCamera.nearClipPlane = 2;
            _mainCamera.farClipPlane = 8000;
        }
    }
}
