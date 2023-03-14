using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Transport;
using MetaPath.DataObjects;


namespace MetaPath.Locations{
    public class PositionManager
    {

        private TransportPositioner _transportPositioner;
        private TransportApi _transportApi;
        private TransportPositionerPointOnGraph _previousPointOnGraph;
        private TransportPositionerPointOnGraph _currentPointOnGraph;
        private bool _isMatchNeeded = false;
        private bool _isPathNeeded = false;

        public bool IsMatchNeeded{
            get { return _isMatchNeeded; }
        }

        public bool IsPathNeeded{
            get { return _isPathNeeded; }
        }


        public PositionManager(TransportPositioner transportPositioner, TransportApi transportApi){
            _transportPositioner = transportPositioner;
            _transportApi = transportApi;
        }

        public void UpdatePosition(Location location){
            _transportPositioner.SetInputCoordinates(location.LatitudeDegrees, location.LongitudeDegrees);
            _isMatchNeeded = true;
            _isPathNeeded = true;
        }
    }
}
