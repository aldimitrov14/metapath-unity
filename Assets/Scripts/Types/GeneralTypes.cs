using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetaPath.ViewObjects;

namespace MetaPath.Types{

    public struct LocationStructure{
        public Vector3 coordinates;
        public Vector3 headingDegrees;
        public bool isCapsuleNeeded;
        public View view;
    }
}