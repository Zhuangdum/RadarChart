using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public RadarMap radarMap;
    public void UpdateCanvas()
    {
        radarMap.rectTransform.sizeDelta = new Vector2(100, 130);
    }
    
    public void UpdateColor()
    {
        radarMap.SetVerticesDirty();
    }

    public void UpdateVertices()
    {
        radarMap.RebuildSubRadar();
    }
}
