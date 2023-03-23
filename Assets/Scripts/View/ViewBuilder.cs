using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MetaPath.Constants;

namespace MetaPath.ViewObjects{
    public class ViewBuilder : MonoBehaviour
    {

        [SerializeField]
        private GameObject _viewScreen;

        private Dropdown _viewDropdown;

        private ViewManager _viewManager;

        private View _currentSelectedView;

        private List<View> _viewList;

        public View CurrentSelectedView{
            get {return _currentSelectedView;}
        }

        void Start()
        {
            _viewManager = new ViewManager();
            _viewDropdown = GameObject.Find(ViewConstants.ViewDropdown).GetComponent<Dropdown>();
            HideViewDropDown();

            _viewManager.CreateView(2, 2, UIConstants.Default);
            _viewManager.CreateView(90, 90, UIConstants.BirdsEye);

            List<Dropdown.OptionData> dropData = new List<Dropdown.OptionData>();

            _viewList = _viewManager.ViewList;

            _currentSelectedView = _viewList[0];

            foreach(var view in _viewList){
                dropData.Add(new Dropdown.OptionData(view.Name));
            }
            
            _viewDropdown.AddOptions(dropData);

            _viewDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        void Update()
        {
            
        }

        void OnDropdownValueChanged(int selectedIndex){ 

        _currentSelectedView = _viewList[selectedIndex];

        }

        public void ShowViewDropDown(){
            _viewScreen.SetActive(true);
        }

        public void HideViewDropDown(){
            _viewScreen.SetActive(false);
        }
    }
}