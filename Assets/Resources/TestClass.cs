//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using UnityEngine;



public class TestClass : MonoBehaviour
{
    
    private UnityEngine.Color initColor;
    
    private UnityEngine.Color otherColor;
    
    private void Start()
    {
        initColor = Color.blue;
        otherColor = Color.red;
    }
    
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.C))
        {
            UnityEngine.Renderer rend = this.GetComponent<Renderer>();
            if ((rend.material.color == initColor))
            {
                rend.material.color = otherColor;
            }
            else
            {
                rend.material.color = initColor;
            }
        }
    }
}
