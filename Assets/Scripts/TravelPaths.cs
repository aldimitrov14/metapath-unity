using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

namespace MetaPath.WebPortal.DataObjects{
public class TravelPath
{
    public string ID { get; set; }
    public string createdAt { get; set; }
    public string createdBy { get; set; }
    public string modifiedAt { get; set; }
    public string modifiedBy { get; set; }
    public string valid_from { get; set; }
    public string valid_to { get; set; }
    public float starting_latitude { get; set; }
    public float starting_longitude { get; set; }
    public float final_latitude { get; set; }
    public float final_longitude { get; set; }
    public string travel_type { get; set; }
    public string routes_ID { get; set; }
}
}
