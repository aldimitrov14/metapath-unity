using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MetaPath.WebPortal;
using MetaPath.WebPortal.DataObjects;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MetaPath.Constants;
using MetaPath.Locations;

namespace MetaPath.Main {

    public class ScreenBartender : MonoBehaviour
    {

        [SerializeField]
        private GameObject _mainObject;

        [SerializeField]
        private GameObject _loadingScreen;

        [SerializeField]
        private GameObject _menu;

        private int _state = UIConstants.StateInitial;
        private List<TravelPath> _travelPaths = new List<TravelPath>();
        private LocationManager _locationManager;

        public LocationManager LocationManager{
            get{ return _locationManager; }
        }

        void Start(){
            _locationManager = new LocationManager();
            _loadingScreen.SetActive(true);
            _menu.SetActive(false);
            _mainObject.SetActive(false);
        }

        void Update()
        {
            if(_state != 1){
                GameObject baseObject = GameObject.Find(UIConstants.BaseObject);
                DataLoader dataLoader = baseObject.GetComponent<DataLoader>();

                if(dataLoader.IsReady == true){
                    _loadingScreen.SetActive(false);
                    _menu.SetActive(true);
                    _state = UIConstants.StateDataLoaded;

                    _travelPaths = dataLoader.DataSet[UIConstants.Value].ToObject<List<TravelPath>>();

                    Dropdown menuDropdown = GameObject.Find(UIConstants.MenuDropdown).GetComponent<Dropdown>();

                    List<Dropdown.OptionData> dropData = new List<Dropdown.OptionData>();

                    dropData.Add(new Dropdown.OptionData(UIConstants.InitialDropdownData));

                    foreach(var travelPath in _travelPaths){
                        dropData.Add(new Dropdown.OptionData(travelPath.routes_ID));
                    }

                    menuDropdown.AddOptions(dropData);

                    menuDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
                }

            }

        }

        void OnDropdownValueChanged(int selectedIndex){
        _loadingScreen.SetActive(true);
        _menu.SetActive(false);    

        TravelManager travelManager = _mainObject.GetComponent<TravelManager>();

        WrldMap wrldMap = _mainObject.GetComponent<WrldMap>();

        TravelPath selectedTravelPath = _travelPaths[selectedIndex-1];

        _locationManager.CreateLocation(selectedTravelPath.starting_latitude, selectedTravelPath.starting_longitude);
        _locationManager.CreateLocation(selectedTravelPath.final_latitude, selectedTravelPath.final_longitude);

        _mainObject.SetActive(true);  

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
