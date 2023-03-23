using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaPath.UI.Objects{
    public class Capsule
    {
        private GameObject _capsule;

        private Material _material;

        public Capsule(Color color, float radius, Transform parent){
            _capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);

            _material = new Material(Shader.Find("Sprites/Default"));
            _material.color = color;
            _capsule.GetComponent<Renderer>().material = _material;
            _capsule.transform.localScale = Vector3.one * radius;
            _capsule.transform.parent = parent;
        }

        public void SetLocalPosition(Vector3 localPosition){
            _capsule.transform.localPosition = localPosition;
        }

        public void SetLocalEulerAngles(Vector3 localEulerAngles){
            _capsule.transform.localEulerAngles = localEulerAngles;
        }

        public void ShowCapsule(){
            _capsule.SetActive(true);
        }

        public void HideCapsule(){
            _capsule.SetActive(false);
        }
    }
}
