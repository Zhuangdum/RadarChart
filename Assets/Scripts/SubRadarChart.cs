using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubRadarChart : Graphic
{
    private VertexHelper vertextHelper;

    //用于记录背景多边形的顶点数组位置
    private Vector2[] vertPos = new Vector2[6];
    
    //多边形类型
    public ShadeType shadeType;

    //更加传入的顶点数据来绘制雷达图
    public void SetRadarVertext(float[] vertArray, float edges)
    {
        if (vertArray.Length == 5)
        {
            float length = edges / 2 / Mathf.Cos(Mathf.Deg2Rad * 54);
            //计算各个顶点的位置
            Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
            Vector4 vector4 = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + edges,
                pixelAdjustedRect.y + edges);
            vertPos = new Vector2[6];
            vertPos[0] = new Vector2(vector4.x+(1-vertArray[0])*edges/2, vector4.y+Mathf.Tan(Mathf.Deg2Rad*54)*(1-vertArray[0])*edges/2);
            vertPos[1] = new Vector2(vector4.x + edges / 2, vector4.y);
            vertPos[2] = new Vector2(vector4.x + edges / 2,
                vector4.y+length*(Mathf.Sin(Mathf.Deg2Rad*54)+vertArray[3]));
            vertPos[3] = new Vector2(vector4.x+edges/2-length*vertArray[4]*Mathf.Cos(Mathf.Deg2Rad*18),
                vector4.y+length*Mathf.Sin(Mathf.Deg2Rad*54)+length*Mathf.Sin(Mathf.Deg2Rad*18)*vertArray[4]);
            vertPos[4] = new Vector2(vector4.x +edges/2*(1+vertArray[1]), vector4.y+length*Mathf.Sin(Mathf.Deg2Rad*54)*(1-vertArray[1]));
            vertPos[5] = new Vector2(vector4.x+edges/2+length*vertArray[2]*Mathf.Cos(Mathf.Deg2Rad*18),
                vector4.y+length*Mathf.Sin(Mathf.Deg2Rad*54)+length*Mathf.Sin(Mathf.Deg2Rad*18)*vertArray[2]);
        }
        else
        {
            //TODO 传入的数据没有按照格式传递
        }
    }

    //重绘UI的Mesh
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (shadeType == ShadeType.Pentagon)
        {
//            DrawPentagon(vh);
            DrawPentagonTest(vh);
            Debug.Log("重绘制子雷达图");
        }
    }

    //绘制五边形
    private void DrawPentagon(VertexHelper vh)
    {
        Color32 color = (Color32) this.color;
        vh.Clear();

        //添加左半边的四边形
        //0
        vh.AddVert(new Vector3(vertPos[0].x, vertPos[0].y), color, new Vector2(0.0f, 0.0f));
        //1
        vh.AddVert(new Vector3(vertPos[1].x, vertPos[1].y), color, new Vector2(0.5f, 0.0f));
        //2
        vh.AddVert(new Vector3(vertPos[2].x, vertPos[2].y), color, new Vector2(0.5f, 1f));
        //3
        vh.AddVert(new Vector3(vertPos[3].x, vertPos[3].y), color, new Vector2(0.0f, 0.5f));

//        //添加右半边的四边形
        //4
        vh.AddVert(new Vector3(vertPos[1].x, vertPos[1].y), color, new Vector2(0.5f, 0f));
        //5
        vh.AddVert(new Vector3(vertPos[4].x, vertPos[4].y), color, new Vector2(1.0f, 0.0f));
        //6
        vh.AddVert(new Vector3(vertPos[5].x, vertPos[5].y), color, new Vector2(1f, 0.5f));
        //7
        vh.AddVert(new Vector3(vertPos[2].x, vertPos[2].y), color, new Vector2(0.5f, 1.0f));


        //添加左半边的三角形
        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);

//        //添加右半边的三角形
        vh.AddTriangle(4, 5, 6);
        vh.AddTriangle(6, 7, 4);
    }
    
    private void DrawPentagonTest(VertexHelper vh)
    {
        Color32 color = (Color32) this.color;
        vh.Clear();

        //添加左半边的四边形
        //0
        vh.AddVert(new Vector3(vertPos[0].x, vertPos[0].y), color, new Vector2(0.0f, 0.0f));
        //1
        vh.AddVert(new Vector3(vertPos[4].x, vertPos[4].y), color, new Vector2(1.0f, 0.0f));
        //2
        vh.AddVert(new Vector3(vertPos[5].x, vertPos[5].y), color, new Vector2(1f, 0.5f));
        //3
        vh.AddVert(new Vector3(vertPos[2].x, vertPos[2].y), color, new Vector2(0.5f, 1f));
        //4
        vh.AddVert(new Vector3(vertPos[3].x, vertPos[3].y), color, new Vector2(0.0f, 0.5f));

        //添加左半边的三角形
        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(0, 2, 3);
        vh.AddTriangle(0, 3, 4);
    }
}