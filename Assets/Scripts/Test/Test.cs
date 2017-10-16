using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public RadarChart radarChart;
    public void UpdateCanvas()
    {
        radarChart.rectTransform.sizeDelta = new Vector2(100, 130);
    }
    
    public void UpdateColor()
    {
        radarChart.SetVerticesDirty();
    }

    public void UpdateVertices()
    {
        radarChart.RebuildSubRadar();
    }

    public void TestClick()
    {
        Debug.Log("you have click the button");
    }
}
