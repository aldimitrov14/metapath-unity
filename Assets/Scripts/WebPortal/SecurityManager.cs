using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MetaPath.Constants;

namespace MetaPath.WebPortal{
    public class SecurityManager
    {
        public IEnumerator GetOAuth2Token()
        {
            string url = System.Environment.GetEnvironmentVariable(WebConstants.EnvOauthUrl);

            UnityWebRequest request = UnityWebRequest.Post(url, WebConstants.EmptyUrl);

            request.SetRequestHeader(WebConstants.Authorization, WebConstants.Basic + Convert.ToBase64String(Encoding.ASCII.GetBytes(System.Environment.GetEnvironmentVariable(WebConstants.EnvClientCredetials))));
            request.SetRequestHeader(WebConstants.ContentType, WebConstants.MimeType);

            string body = WebConstants.GrantType;
            byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                yield return request.error;
            }
            else
            {
                string responseText = Encoding.UTF8.GetString(request.downloadHandler.data);

                JObject responseJson = JObject.Parse(responseText);
                string accessToken = responseJson[WebConstants.AccessToken].ToString();

                yield return accessToken;
            }
        }
    }
}