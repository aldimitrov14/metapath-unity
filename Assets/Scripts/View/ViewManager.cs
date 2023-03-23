using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaPath.ViewObjects{
    public class ViewManager
    {

        private List<View> _viewList = new List<View>();

        public List<View> ViewList{
            get {return _viewList;}
            set {_viewList = value;}
        }


        public View CreateView(float eulerAngleX, int groundHeight, string name){
            View view = new View(eulerAngleX, groundHeight, name);

            _viewList.Add(view);

            return view;
        }
    }
}
