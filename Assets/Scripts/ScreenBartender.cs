using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MetaPath.WebPortal;
using MetaPath.WebPortal.DataObjects;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MetaPath.Main {

    public class ScreenBartender : MonoBehaviour
    {

        [SerializeField]
        private GameObject _mainObject;

        [SerializeField]
        private GameObject _loadingScreen;

        [SerializeField]
        private GameObject _menu;

        private int _state = 0;
        private List<TravelPath> _travelPaths = new List<TravelPath>();

        void Start(){
            _loadingScreen.SetActive(true);
            _menu.SetActive(false);
            _mainObject.SetActive(false);
        }

        void Update()
        {
            if(_state != 1){
                GameObject baseObject = GameObject.Find("Base");
                DataLoader dataLoader = baseObject.GetComponent<DataLoader>();

                if(dataLoader.IsReady == true){
                    _loadingScreen.SetActive(false);
                    _menu.SetActive(true);
                    _state = 1;

                    _travelPaths = dataLoader.DataSet["value"].ToObject<List<TravelPath>>();

                    Dropdown menuDropdown = GameObject.Find("MenuDropdown").GetComponent<Dropdown>();

                    List<Dropdown.OptionData> dropData = new List<Dropdown.OptionData>();

                    dropData.Add(new Dropdown.OptionData("Please Select a Travel Path"));

                    foreach(var travelPath in _travelPaths){
                        dropData.Add(new Dropdown.OptionData(travelPath.routes_ID));
                    }

                    menuDropdown.AddOptions(dropData);

                    menuDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
                }

            }

        }

        void OnDropdownValueChanged(int selectedIndex){
        _mainObject.SetActive(true);  
        _loadingScreen.SetActive(true);
        _menu.SetActive(false);    

        TravelManager travelManager = _mainObject.GetComponent<TravelManager>();

        WrldMap wrldMap = _mainObject.GetComponent<WrldMap>();

        TravelPath selectedTravelPath = _travelPaths[selectedIndex-1];

        travelManager.LocationManager.CreateLocation(selectedTravelPath.starting_latitude, selectedTravelPath.starting_longitude);
        travelManager.LocationManager.CreateLocation(selectedTravelPath.final_latitude, selectedTravelPath.final_longitude);

        wrldMap.StartingCameraLatitudeDegrees = selectedTravelPath.starting_latitude;
        wrldMap.StartingCameraLongitudeDegrees = selectedTravelPath.starting_longitude;

        }

        public void HideLoadingScreen(){
            _loadingScreen.SetActive(false);
        }

        public void ShowLoadingScreen(){
            _loadingScreen.SetActive(true);
        }
    }
}
