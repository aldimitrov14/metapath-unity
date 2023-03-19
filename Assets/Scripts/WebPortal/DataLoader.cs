using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MetaPath.WebPortal.DataObjects;

namespace MetaPath.WebPortal{
    public class DataLoader : MonoBehaviour
    {
        [SerializeField]
        private string entity;

        [Tooltip("Development env: 1, Localhost: 2, No-Internet: 3")]
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

            string url = "";

            // testing purposes 
            switch(mode){
                case 1:
                url = System.Environment.GetEnvironmentVariable("CLOUD_URL") + entity;
                break;
                case 2:
                url = System.Environment.GetEnvironmentVariable("LOCAL_URL") + entity;
                break;
                case 3:
                _isReady = true;
                _dataSet = JObject.Parse(System.Environment.GetEnvironmentVariable("SAMPLE_DATA"));
                yield break;
            }

            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Authorization", "Bearer " + accessToken);

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
                Debug.LogWarning("Switching to No-Internet Env");
                _isReady = true;
                _dataSet = JObject.Parse(System.Environment.GetEnvironmentVariable("SAMPLE_DATA"));
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
