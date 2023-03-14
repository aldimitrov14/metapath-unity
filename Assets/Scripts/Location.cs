using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaPath.DataObjects{
public class Location 
{

        private double _latitudeDegrees;
        private double _longitudeDegrees;
        private double _headingDegrees;

        public double LatitudeDegrees{
                get { return _latitudeDegrees; }
                set { _latitudeDegrees = value; }
        }
        public double LongitudeDegrees{
                get { return _longitudeDegrees; }
                set { _longitudeDegrees = value; }
        }
        public double HeadingDegrees{
                get { return _headingDegrees; }
                set { _headingDegrees = value; }
        }

        public Location(double latitudeDegrees, double longitudeDegrees, double heading)
        {
                LatitudeDegrees = latitudeDegrees;
                LongitudeDegrees = longitudeDegrees;
                HeadingDegrees = heading;
        }
    };
}
