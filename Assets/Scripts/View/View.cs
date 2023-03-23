using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaPath.ViewObjects{
public class View 
{
    public float EulerAngleX  {get; set;}
    public int GroundHeight  {get; set;}
    public string Name {get; set;}

    public View(float eulerAngleX, int groundHeight, string name){
        GroundHeight = groundHeight;
        EulerAngleX = eulerAngleX;
        Name = name;
    }
}
}