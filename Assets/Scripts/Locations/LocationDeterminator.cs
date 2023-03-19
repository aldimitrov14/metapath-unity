﻿using System;
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

            locationStructure.coordinates = AdjustLocationHeight(currentLocation);
            locationStructure.headingDegrees = CreateHeadingVector(CorrectHeadingDegrees(headingDegrees));

            return locationStructure;
        }

        private double CorrectHeadingDegrees(double headingDegrees){
            if ((float)headingDegrees != (float)headingDegrees){
                return LocationConstants.NormalizedDegrees;
            } else {
                return headingDegrees;
            }
        }

        private Vector3 AdjustLocationHeight(Vector3 location){
            location.y = location.y + LocationConstants.GroundHeight;
            
            return location;
        }

        private Vector3 CreateHeadingVector(double headingDegrees){
            return new Vector3(LocationConstants.EulerAxisX, (float)headingDegrees, LocationConstants.EulerAxisZ);
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
