using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetaPath.DataObjects;

namespace MetaPath.Locations{
    public class LocationManager
    {
        private List<Location> _locationList;

        public List<Location> LocationList{
            get { return _locationList; }
        }

        public LocationManager(){
            _locationList = new List<Location>();
        }

        public void CreateLocation(double latitudeDegrees, double longitudeDegrees, double headingDegrees){
            _locationList.Add(new Location(latitudeDegrees, longitudeDegrees, headingDegrees));
        }
    }
}
