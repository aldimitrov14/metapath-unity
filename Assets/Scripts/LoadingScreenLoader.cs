using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetaPath.WebPortal;

namespace MetaPath.Main{
    public class LoadingScreenLoader : MonoBehaviour
    {

        [SerializeField]
        private GameObject LoadingScreen;

        void Start()
        {
            LoadingScreen.SetActive(true);
        }

        public void OnApplicationQuit()
        {
            LoadingScreen.SetActive(false);
        }

        public void HideLoadingScreen(){
            LoadingScreen.SetActive(false);
        }

        public void ShowLoadingScreen(){
            LoadingScreen.SetActive(true);
        }


    }
}