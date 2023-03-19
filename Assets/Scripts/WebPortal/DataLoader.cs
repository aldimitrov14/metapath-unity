using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MetaPath.WebPortal.DataObjects;
using MetaPath.Constants;

namespace MetaPath.WebPortal{
    public class DataLoader : MonoBehaviour
    {
        [SerializeField]
        private string entity;

        [Tooltip(WebConstants.ModeTooltip)] 
        [Range(1, 3)]
        [SerializeField]
        private int mode;
        private JObject _dataSet;
        private bool _isReady = false;

        public JObject DataSet{
            get {return _dataSet;}
        }

        public bool IsReady{
            get {return _isReady;}
        }

        void Start() {
            StartCoroutine(GetTravelData());
        }

        public IEnumerator GetTravelData()
        {

            IEnumerator tokenCoroutine = new SecurityManager().GetOAuth2Token();
            yield return StartCoroutine(tokenCoroutine);
            string accessToken = (string)tokenCoroutine.Current;

            string url = WebConstants.EmptyUrl;

            // testing purposes 
            switch(mode){
                case WebConstants.ModeDevelopmentEnv:
                url = System.Environment.GetEnvironmentVariable(WebConstants.EnvCloudUrl) + entity;
                break;
                case WebConstants.ModeLocalEnv:
                url = System.Environment.GetEnvironmentVariable(WebConstants.EnvLocalUrl) + entity;
                break;
                case WebConstants.ModeNoInternet:
                _isReady = true;
                _dataSet = JObject.Parse(System.Environment.GetEnvironmentVariable(WebConstants.EnvSampleData));
                yield break;
            }

            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader(WebConstants.Authorization, WebConstants.Bearer + accessToken);

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
                Debug.LogWarning(WebConstants.NoInternetMessage);
                _isReady = true;
                _dataSet = JObject.Parse(System.Environment.GetEnvironmentVariable(WebConstants.EnvSampleData));
                yield return request.error;
            }
            else
            {
                string responseText = Encoding.UTF8.GetString(request.downloadHandler.data);
                JObject responseJson = JObject.Parse(responseText);

                // since C# is a beautiful language and to await until the Coroutine is finished to get the data 
                // of the coroutine is close to impossible, we will asign the data to two variables and use them to check against 
                // whether the data is loaded, presumably in the Update function (if IsReady == true) => work with DataSet
                _dataSet = responseJson;
                _isReady = true;
                yield return responseJson;
            }
        }
    }
}
