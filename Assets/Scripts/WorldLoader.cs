using Wrld;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetaPath.Main{
    public class WorldLoader : MonoBehaviour
    {

        [SerializeField]
        private GameObject Root;

        void Start()
        {
            Root.SetActive(true);
        }

        public void OnApplicationQuit()
        {
            Root.SetActive(false);
        }

    }
}
