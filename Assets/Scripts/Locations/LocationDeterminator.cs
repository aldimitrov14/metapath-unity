using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Transport;
using MetaPath.DataObjects;
using MetaPath.Locations;
using MetaPath.Types;
using MetaPath.Constants;
using MetaPath.ViewObjects;
using MetaPath.UI.Objects;

namespace MetaPath.Locations{
    public class LocationDeterminator
    {
        private TransportApi _transportApi;
        private SpacesApi _spacesApi; 


        public LocationDeterminator(TransportApi transportApi, SpacesApi spacesApi){
            _transportApi = transportApi;
            _spacesApi = spacesApi;
        }

        public LocationStructure DetermineNextPossiblePosition( double elapsedTime, 
                                                                double previousTime, 
                                                                double currentTime,
                                                                TransportPathfindResult path){

            var locationStructure = new LocationStructure();

            var parameterizedDistanceAlongPath = CalculateDistanceAlongPath(elapsedTime, previousTime, currentTime);
            var pointEcef = _transportApi.GetPointEcefOnPath(path, parameterizedDistanceAlongPath);
            var directionEcef = _transportApi.GetDirectionEcefOnPath(path, parameterizedDistanceAlongPath);
            var headingDegrees = _spacesApi.HeadingDegreesFromDirectionAtPoint(directionEcef, pointEcef);
            var currentLocation = _spacesApi.GeographicToWorldPoint(LatLongAltitude.FromECEF(pointEcef));

            GameObject baseObject = GameObject.Find(UIConstants.BaseObject);
            ViewBuilder viewBuilder = baseObject.GetComponent<ViewBuilder>();

            locationStructure.coordinates = AdjustLocationHeight(currentLocation, viewBuilder.CurrentSelectedView.GroundHeight);
            locationStructure.headingDegrees = CreateHeadingVector(CorrectHeadingDegrees(headingDegrees), viewBuilder.CurrentSelectedView.EulerAngleX);
            locationStructure.view = viewBuilder.CurrentSelectedView;

            if (viewBuilder.CurrentSelectedView.Name != UIConstants.Default){
                locationStructure.isCapsuleNeeded = true;
            }else{
                locationStructure.isCapsuleNeeded = false;
            }

            return locationStructure;
        }

        private double CorrectHeadingDegrees(double headingDegrees){
            if ((float)headingDegrees != (float)headingDegrees){
                return LocationConstants.NormalizedDegrees;
            } else {
                return headingDegrees;
            }
        }

        private Vector3 AdjustLocationHeight(Vector3 location, int groundHeight){
            location.y = location.y + groundHeight;
            
            return location;
        }

        private Vector3 CreateHeadingVector(double headingDegrees, float eulerAxisX){
            return new Vector3((float)eulerAxisX, (float)headingDegrees, LocationConstants.EulerAxisZ);
        }

        private double CalculateDistanceAlongPath(double elapsedTime, double previousTime, double currentTime)
        {
            var delta = currentTime - previousTime;

            if (delta > LocationConstants.InitialDelta)
            {
                return Math.Min(Math.Max((elapsedTime - previousTime) / delta, LocationConstants.InitialDelta), LocationConstants.MaxDelta);
            }
            else
            {
                return LocationConstants.InitialDelta;
            }
        }
    }
}
