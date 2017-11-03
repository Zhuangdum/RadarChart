using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
     (0, 1)            (1, 1)
     
     
     
     
     
     (0, 0)            (1, 0)
 */

public class TestVertColor : Graphic
{
    public Color startColor;
    public Color endColor;
    
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
        Vector4 vector4 = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
//        Color32 color = (Color32) this.color;
        vh.Clear();
        vh.AddVert(new Vector3(vector4.x, vector4.y), startColor, new Vector2(0.0f, 0.0f));
        vh.AddVert(new Vector3(vector4.x, vector4.w), endColor, new Vector2(0.0f, 1f));
        vh.AddVert(new Vector3(vector4.z, vector4.w), endColor, new Vector2(1f, 1f));
        vh.AddVert(new Vector3(vector4.z, vector4.y), startColor, new Vector2(1f, 0.0f));
        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }
}
