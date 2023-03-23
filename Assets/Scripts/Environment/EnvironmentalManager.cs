using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;

namespace MetaPath.Env{
                 
    public class EnvironmentalManager : MonoBehaviour
    {
        void Awake(){
            System.Environment.SetEnvironmentVariable("SAMPLE_DATA", GenerateTravelPaths());
            System.Environment.SetEnvironmentVariable("CLOUD_URL", GenerateCloudUrl());
            System.Environment.SetEnvironmentVariable("LOCAL_URL", GenerateLocalUrl());
            System.Environment.SetEnvironmentVariable("CLIENT_CREDENTIALS", GenerateClientCredentials());
            System.Environment.SetEnvironmentVariable("OAUTH_URL", GenerateOAuthLink());
        }

        private string GenerateOAuthLink(){
            return "https://1f5e5a20trial.authentication.us10.hana.ondemand.com/oauth/token";
        }

        private string GenerateClientCredentials(){
            return "sb-metapath-1f5e5a20trial-dev!t114826:l7QbYbjpsqvsXfxYKvuur/l8D2E=";
        }

        private string GenerateCloudUrl(){
            return "https://1f5e5a20trial-dev-metapath-srv.cfapps.us10-001.hana.ondemand.com/api/";
        }

        private string GenerateLocalUrl(){
            return "localhost:4004/api/";
        }

        private string GenerateTravelPaths(){
            return @"{
                      ""@odata.context"": ""$metadata#TravelPaths"",
                        ""value"": [
                        {
                        ""ID"": ""95xs333d-799e-4e17-adaf-6cb827bc0ca3"",
                        ""createdAt"": ""2023-03-15T08:03:21.239Z"",
                        ""createdBy"": ""alice"",
                        ""modifiedAt"": ""2023-03-15T08:03:21.239Z"",
                        ""modifiedBy"": ""alice"",
                        ""valid_from"": ""2023-03-15"",
                        ""valid_to"": ""9999-01-01"",
                        ""starting_latitude"": 42.673659,
                        ""starting_longitude"": 23.282007,
                        ""final_latitude"": 42.665939,
                        ""final_longitude"": 23.286181,
                        ""travel_type"": null,
                        ""routes_ID"": "" Travel Path - 1""
                        },
                        {
                        ""ID"": ""95xs333d-799e-9e17-adaf-6cb827bc08df"",
                        ""createdAt"": ""2023-03-15T08:03:21.239Z"",
                        ""createdBy"": ""alice"",
                        ""modifiedAt"": ""2023-03-15T08:03:21.239Z"",
                        ""modifiedBy"": ""alice"",
                        ""valid_from"": ""2023-03-15"",
                        ""valid_to"": ""9999-01-01"",
                        ""starting_latitude"": 42.675559,
                        ""starting_longitude"": 23.288167,
                        ""final_latitude"": 42.673250,
                        ""final_longitude"": 23.291371,
                        ""travel_type"": null,
                        ""routes_ID"": ""(Local) Travel Path - 2""
                        }
                    ]
                }";
        }

    }
}
