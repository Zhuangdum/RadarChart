using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(RadarChart))]
public class TestInspector : Editor
{
    public override void OnInspectorGUI()
    {        
        DrawDefaultInspector();
        
        RadarChart myScript = (RadarChart)target;
        if(GUILayout.Button("PopulateMesh"))
        {
            Debug.Log("test inspector GUI");
        }
    }
}