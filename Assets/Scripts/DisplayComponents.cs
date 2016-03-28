using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
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

                var obj = new UnityEditor.SerializedObject(gameObject.GetComponent<MeshRenderer>());
                var it = obj.GetIterator();
                while (it.NextVisible(true)) {
                    text.text += it.name + " = " + GetPropertyValue(it) + "\n";
                }
                //foreach (var field in t.GetMembers(BindingFlags.Public | BindingFlags.DeclaredOnly)) {
                //    text.text += field.Name + "\n";

                //    if (field.Name.Equals("material")) {
                //       // field.PropertyType.GetProperty("color").SetValue(rend.material,Color.white, null);
                //    }
                //}
                
                colorPicker.transform.SetParent(gameObject.transform);
                colorPicker.SetActive(true);
            }
        }
    }

    public static void SetPropertyValue(SerializedProperty prop, object value) {
        if (prop == null) throw new System.ArgumentNullException("prop");

        switch (prop.propertyType) {
            case SerializedPropertyType.Integer:
                prop.intValue = (int)value;
                break;
            case SerializedPropertyType.Boolean:
                prop.boolValue = (bool)value;
                break;
            case SerializedPropertyType.Float:
                prop.floatValue = (float)value;
                break;
            case SerializedPropertyType.String:
                prop.stringValue = (string)value;
                break;
            case SerializedPropertyType.Color:
                prop.colorValue = (Color)value;
                break;
            case SerializedPropertyType.ObjectReference:
                prop.objectReferenceValue = value as UnityEngine.Object;
                break;
            case SerializedPropertyType.LayerMask:
                prop.intValue = (value is LayerMask) ? ((LayerMask)value).value : (int)value;
                break;
            case SerializedPropertyType.Enum:
                prop.enumValueIndex = (int)value;
                break;
            case SerializedPropertyType.Vector2:
                prop.vector2Value = (Vector2)value;
                break;
            case SerializedPropertyType.Vector3:
                prop.vector3Value = (Vector3)value;
                break;
            case SerializedPropertyType.Vector4:
                prop.vector4Value = (Vector4)value;
                break;
            case SerializedPropertyType.Rect:
                prop.rectValue = (Rect)value;
                break;
            case SerializedPropertyType.ArraySize:
                prop.arraySize = (int)value;
                break;
            case SerializedPropertyType.Character:
                prop.intValue = (int)value;
                break;
            case SerializedPropertyType.AnimationCurve:
                prop.animationCurveValue = value as AnimationCurve;
                break;
            case SerializedPropertyType.Bounds:
                prop.boundsValue = (Bounds)value;
                break;
            case SerializedPropertyType.Gradient:
                throw new System.InvalidOperationException("Can not handle Gradient types.");
        }
    }

    public static object GetPropertyValue(SerializedProperty prop) {
        if (prop == null) throw new System.ArgumentNullException("prop");

        if (prop.isArray) {
            return prop.arraySize;
        }

        switch (prop.propertyType) {
            case SerializedPropertyType.Integer:
                return prop.intValue;
            case SerializedPropertyType.Boolean:
                return prop.boolValue;
            case SerializedPropertyType.Float:
                return prop.floatValue;
            case SerializedPropertyType.String:
                return prop.stringValue;
            case SerializedPropertyType.Color:
                return prop.colorValue;
            case SerializedPropertyType.ObjectReference:
                return prop.objectReferenceValue;
            case SerializedPropertyType.LayerMask:
                return (LayerMask)prop.intValue;
            case SerializedPropertyType.Enum:
                return prop.enumValueIndex;
            case SerializedPropertyType.Vector2:
                return prop.vector2Value;
            case SerializedPropertyType.Vector3:
                return prop.vector3Value;
            case SerializedPropertyType.Vector4:
                return prop.vector4Value;
            case SerializedPropertyType.Rect:
                return prop.rectValue;
            case SerializedPropertyType.ArraySize:
                return prop.intValue;
            case SerializedPropertyType.Character:
                return (char)prop.intValue;
            case SerializedPropertyType.AnimationCurve:
                return prop.animationCurveValue;
            case SerializedPropertyType.Bounds:
                return prop.boundsValue;
            case SerializedPropertyType.Gradient:
                throw new System.InvalidOperationException("Can not handle Gradient types.");
        }
        return null;
    }
}
