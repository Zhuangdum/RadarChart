using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(RadarMap))]
public class TestInspector : Editor
{
    public override void OnInspectorGUI()
    {        
        DrawDefaultInspector();
        
        RadarMap myScript = (RadarMap)target;
        if(GUILayout.Button("PopulateMesh"))
        {
            myScript.subRaderMap[0].Rebuild(CanvasUpdate.Layout);
        }
    }
}