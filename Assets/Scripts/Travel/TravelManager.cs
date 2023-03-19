using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Transport;
using MetaPath.DataObjects;
using MetaPath.Locations;
using MetaPath.Cameras;
using MetaPath.WebPortal;
using MetaPath.Constants;

namespace MetaPath.Main{
    public class TravelManager : MonoBehaviour
    {
        private LocationManager _locationManager;
        private List<Location> _locationList;
        private LocationDeterminator _locationDeterminator;
        private CameraManager _cameraManager;
        private TravelTimeDeterminator _travelTimeDeterminator;
        private PositionManager _positionManager;
        private TransportPositioner _transportPositioner;
        private TransportPositionerPointOnGraph _previousPointOnGraph;
        private TransportPositionerPointOnGraph _currentPointOnGraph;
        private TransportPathfindResult _travelPath;
        private Camera _mainCamera;
        private TransportApi _transportApi;
        private SpacesApi _spacesApi;
        private int _currentLocationIndex;
        private double _elapsedTime;
        private double _previousTravelTime;
        private double _currentTravelTime;
        private double _expectedTravelTime;
        private bool _isPathNeeded;
        private bool _isMatchNeeded;
        private bool _isLoadingScreenShown = true;

        private void OnEnable()
        {
            
            InitWrldAPIs();

            InitLocationManagers();

            CreateTravelPath();

            CreateMainCamera();

            InitTravelVariables();

            InitTransportPositioner();

            InitTravelManagers();

            SetNextLocation();
            
        }

        private void InitTravelManagers(){
            _travelTimeDeterminator = new TravelTimeDeterminator(_locationList);

            _previousPointOnGraph = TransportPositionerPointOnGraph.MakeEmpty();
            
            _currentPointOnGraph = TransportPositionerPointOnGraph.MakeEmpty();

            _positionManager = new PositionManager(_transportPositioner, _transportApi);
        }

        private void InitLocationManagers(){
            GameObject baseObject = GameObject.Find(TravelConstants.BaseObject);
            ScreenBartender screenBartender = baseObject.GetComponent<ScreenBartender>();
            _locationManager = screenBartender.LocationManager;
            _locationDeterminator = new LocationDeterminator(_transportApi, _spacesApi);

            Debug.Log(screenBartender.LocationManager);

            Debug.Log(screenBartender.LocationManager.LocationList.Count);
        }

        private void InitWrldAPIs(){
            _transportApi = Api.Instance.TransportApi;
            _spacesApi = Api.Instance.SpacesApi;
        }

        private void CreateMainCamera(){
            _cameraManager = new CameraManager(UnityEngine.Camera.main);
            _mainCamera = _cameraManager.MainCamera;
        }

        private void InitTravelVariables(){
            _currentLocationIndex = TravelConstants.InitialLocationIndex;
            _elapsedTime = TravelConstants.InitialTime;
            _previousTravelTime = TravelConstants.InitialTime;
            _currentTravelTime = TravelConstants.InitialTime;
            _expectedTravelTime = TravelConstants.InitialExpectedTravelTime;
            _travelPath = null;
        }

        private void CreateTravelPath(){
            _locationList = _locationManager.LocationList;
        }

        private void InitTransportPositioner(){
            _transportPositioner = _transportApi.CreatePositioner(new TransportPositionerOptionsBuilder()
                .SetInputCoordinates(_locationList[0].LatitudeDegrees, _locationList[0].LongitudeDegrees)
                .Build());

            _transportPositioner.OnPointOnGraphChanged += OnPointOnGraphChanged;    
        }

        private void OnDisable()
        {
            _transportPositioner.OnPointOnGraphChanged -= OnPointOnGraphChanged;
            _transportPositioner.Discard();
            _transportPositioner = null;
            _previousPointOnGraph = null;
            _currentPointOnGraph = null;
            _travelPath = null;
        }

        private void Update()
        {   
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _expectedTravelTime)
            {
                NextInput();
            }

            UpdateCamera();
        }


        private void NextInput()
        {
            _expectedTravelTime += _travelTimeDeterminator.CalculateTravelDuration();

            _currentLocationIndex = _travelTimeDeterminator.CurrentLocationIndex;

            SetNextLocation();
        }

        private void SetNextLocation()
        {
            _positionManager.UpdatePosition(_locationList[_currentLocationIndex]);
            _isMatchNeeded = _positionManager.IsMatchNeeded;
            _isPathNeeded = _positionManager.IsPathNeeded;
        }

        private void OnPointOnGraphChanged()
        {
            if (_isMatchNeeded && 
                _transportPositioner.IsMatched())
            {
                _previousTravelTime = _currentTravelTime;
                _currentTravelTime = _expectedTravelTime;
                _previousPointOnGraph = new TransportPositionerPointOnGraph(_currentPointOnGraph);
                _currentPointOnGraph = _transportPositioner.GetPointOnGraph();
                _isMatchNeeded = false;
            }

            if (_isPathNeeded &&
                _currentPointOnGraph.IsMatched &&
                _previousPointOnGraph.IsMatched)
            {
                var path = _transportApi.FindShortestPath(new TransportPathfindOptionsBuilder()
                    .SetPointOnGraphA(_previousPointOnGraph)
                    .SetPointOnGraphB(_currentPointOnGraph)
                    .Build());

                if (path.IsPathFound)
                {
                    _isPathNeeded = false;
                    _travelPath = path;
                }
            }
        }

        private void UpdateCamera()
        {

            if (_travelPath == null)
            {
                return;
            }

            HideLoadingScreen();
            
            var location = _locationDeterminator.DetermineNextPossiblePosition(_elapsedTime, _previousTravelTime, _currentTravelTime, _travelPath);

            _mainCamera.transform.localPosition = location.coordinates;

            _mainCamera.transform.localEulerAngles = location.headingDegrees;
        }

        private void HideLoadingScreen(){
            if(_isLoadingScreenShown == true){
                 GameObject baseObject = GameObject.Find(TravelConstants.BaseObject);
                 ScreenBartender screenBartender = baseObject.GetComponent<ScreenBartender>();
                 screenBartender.HideLoadingScreen();
                 _isLoadingScreenShown = false;
            }
        }
    }
}
