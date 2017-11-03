using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebug : MonoBehaviour 
{
    private void Update()
    {
        
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 100, 100, 100),"log"))
        {
            Debug.Log("debug");
        }
    }
}
