using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayComponents : MonoBehaviour {

    GameObject colorPicker;
    Text text;
	// Use this for initialization
	void Awake () {
	    colorPicker = GameObject.Find("ColorPicker");
	    text = GameObject.Find("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseDown() {
        foreach (var component in GetComponents(typeof(Component))) {
            var rend = component as Renderer;
            Type t = component.GetType();
            if (rend != null) {

                foreach (var field in t.GetProperties()) {
                    text.text += field.Name + "\n";

                    if (field.Name.Equals("material")) {
                        field.PropertyType.GetProperty("color").SetValue(rend.material,Color.white, null);
                    }
                }
                colorPicker.transform.SetParent(gameObject.transform);
                colorPicker.SetActive(true);
            }
        }
    }
}
