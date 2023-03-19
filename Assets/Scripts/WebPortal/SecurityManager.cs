using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MetaPath.WebPortal{
    public class SecurityManager
    {
        public IEnumerator GetOAuth2Token()
        {
            // Construct the request URL
            string url = System.Environment.GetEnvironmentVariable("OAUTH_URL");

            // Create a UnityWebRequest object
            UnityWebRequest request = UnityWebRequest.Post(url, "");

            // Set the request headers
            request.SetRequestHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Environment.GetEnvironmentVariable("CLIENT_CREDENTIALS"))));
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            // Set the request body
            string body = "grant_type=client_credentials";
            byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Send the request
            yield return request.SendWebRequest();

            // Check for errors
            if (request.isNetworkError || request.isHttpError)
            {
                yield return request.error;
            }
            else
            {
                string responseText = Encoding.UTF8.GetString(request.downloadHandler.data);

                JObject responseJson = JObject.Parse(responseText);
                string accessToken = responseJson["access_token"].ToString();

                yield return accessToken;
            }
        }
    }
}