using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Transport;
using MetaPath.DataObjects;
using MetaPath.Constants;


namespace MetaPath.Locations{
    public class TravelTimeDeterminator{
        private List<Location> _locationList;
        private int _currentLocationIndex;

        public List<Location> LocationList{
            get { return _locationList; }
            set { _locationList = value; }
        }

        public int CurrentLocationIndex{
            get { return _currentLocationIndex; }
        }

        public TravelTimeDeterminator(List<Location> locationList){
            _locationList = locationList;
            _currentLocationIndex = TravelConstants.InitialLocationIndex;
        }


        public double CalculateTravelDuration(){

            double travelDuration = TravelConstants.InitialTravelDuration;

            var previousCoordinates = _locationList[_currentLocationIndex];

            _currentLocationIndex++;

            if (_currentLocationIndex >= _locationList.Count)
            {
                _currentLocationIndex = TravelConstants.InitialLocationIndex;
            }

            var nextCoordinates = _locationList[_currentLocationIndex];

            var distance = GetDistance(previousCoordinates.LongitudeDegrees,previousCoordinates.LatitudeDegrees, 
                                       nextCoordinates.LongitudeDegrees, nextCoordinates.LatitudeDegrees);

            travelDuration = DetermineTravelDuration(distance);

            return travelDuration;

        }
        ///<summary>
        /// formula to calculate the distance between two points on the GeoCoordinate system
        /// This method is used to calculate the distance between two points and roughly determine the duration that our travel will be between two points 
        /// This method DOES NOT TAKE into consideration the transport network of the WRLD3D API and it just uses the raw distance between two points
        /// This method is taken from the GeoCoordinate library of C# with the idea of later improving the code so it follows the Haversine formula
        /// references: https://stackoverflow.com/questions/6366408/calculating-distance-between-two-latitude-and-longitude-geocoordinates
        /// note: for now this function does not to be precise, but to give a overview of the distance, refer to DetermineTravelDuration method
        ///</summary>
        private double GetDistance(double startingLongitude, double startingLatitude, double endingPointLongitude, double endingPointLatitude)
        {
        var d1 = startingLatitude * (Math.PI / 180.0);
        var num1 = startingLongitude * (Math.PI / 180.0);
        var d2 = endingPointLatitude * (Math.PI / 180.0);
        var num2 = endingPointLongitude * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
        
        return TravelConstants.EarthRadius * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        ///<summary>
        /// This method gets the first two digits of the distance and uses them as a travelDuration 
        /// In the case that the distance is over 500 meters using the GetDistance method, the distance is cut in half
        /// this is done in order not to make the travel "too slow"
        ///</summary>
        private int DetermineTravelDuration(double distance){



            int travelDuration = Convert.ToInt16(distance.ToString().Substring(0, 2));

            if (travelDuration > 50){
                travelDuration /= 2;
            }

           return travelDuration;
        }
    }
}
