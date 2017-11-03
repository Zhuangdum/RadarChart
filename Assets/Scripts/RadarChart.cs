using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShadeType
{
    Pentagon, //五边形
    Hexagon //六边形
}

/*
    顶点位置示意图
    
                2|7
                 |
                 |
        3        |       6
                 |
                 |
            0   1|4    5
    
    UV坐标示意图
    
        (0.0,1)-----------(1, 1)
            |                |
            |                |
            |                |
            |                |
            |                |
            |                |
        (0.0,0.0)---------(1.0,0.0)
    
    三角形分布示意图
    
        x, w(1)-----------z, w(2)
        |                   |                      
        |                   |  
        |                   |  
        |                   |  
        |                   |  
        |                   |  
        x, y(0)-----------z, y(3)
    
    
    五边形顶点位置顺序示意图
    
                3
           
          
         4              2
         
         
         
             0       1    
    
*/
public class RadarChart : Graphic
{
    private VertexHelper vertextHelper;

    //用于记录背景多边形的顶点数组位置
    private Vector2[] vertPos;

    //多边形边长
    public float edges = 100f;
    
    //多边形类型
    public ShadeType shadeType;

    //顶点文本
    public List<Text> vertText;

    //子多边形的顶点位置数据
    public List<VertPosition> vertPosList;

    //重绘UI的Mesh
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (shadeType == ShadeType.Pentagon)
        {
            DrawPentagon(vh);
            //设置每个顶点的文字说明
            if (vertText.Count >= 5)
            {
                vertText[0].rectTransform.anchoredPosition = vertPos[0];
                vertText[1].rectTransform.anchoredPosition = vertPos[2];
                vertText[2].rectTransform.anchoredPosition = vertPos[3];
                vertText[3].rectTransform.anchoredPosition = vertPos[4];
                vertText[4].rectTransform.anchoredPosition = vertPos[5];
            }
        }
        else if (shadeType == ShadeType.Hexagon)
        {
            DrawHexagon(vh);
        }
        else
        {
            DrawPentagon(vh);
        }
    }

    //重新绘制子雷达图
    public void RebuildSubRadar()
    {
        //绘制子雷达图的模样
        for (int i = 0; i < vertPosList.Count; i++)
        {
            //重新绘制子雷达图
            vertPosList[i].RebuildSubRadar(edges);
        }
    }

    //绘制五边形
    private void DrawPentagon(VertexHelper vh)
    {
        Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
        Color32 color = (Color32) this.color;
        vh.Clear();
        Vector4 vector4 = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + edges,
            pixelAdjustedRect.y + edges);
        Debug.Log("初始位置：x: " + vector4.x + "  y:" + vector4.y + "  height:" + pixelAdjustedRect.height + "  width:" +
                  pixelAdjustedRect.width);

        vertPos = new Vector2[6];
        vertPos[0] = new Vector2(vector4.x, vector4.y);
        vertPos[1] = new Vector2(vector4.x + edges / 2, vector4.y);
        vertPos[2] = new Vector2(vector4.x + edges / 2,
            vector4.y + edges * (float) Math.Sin(Mathf.Deg2Rad * 72) + edges * (float) Math.Sin(Mathf.Deg2Rad * 36));
        vertPos[3] = new Vector2(vector4.x - edges * (float) Math.Cos(Mathf.Deg2Rad * 72),
            vector4.y + edges * (float) Math.Sin(Mathf.Deg2Rad * 72));
        vertPos[4] = new Vector2(vector4.x + edges, vector4.y);
        vertPos[5] = new Vector2(vector4.x + edges + edges * (float) Math.Cos(Mathf.Deg2Rad * 72),
            vector4.y + edges * (float) Math.Sin(Mathf.Deg2Rad * 72));


        //顶点位置

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
    
    private void DrawHexagon(VertexHelper vh)
    {
        //TODO 待处理
    }
}

[Serializable]
public class VertPosition
{
    //0
    [Range(0f, 1f)]
    [SerializeField] private float vert1;
    //1
    [Range(0f, 1f)]
    [SerializeField] private float vert2;
    //2
    [Range(0f, 1f)]
    [SerializeField] private float vert3;
    //3
    [Range(0f, 1f)]
    [SerializeField] private float vert4;
    //4
    [Range(0f, 1f)]
    [SerializeField] private float vert5;
    
    /*
     * 数组对应的顶点位置
     *           3           
     *
     * 
     *  4                  2 
     *
     *
     * 
     *       0        1
     */
    
    //子雷达图的组件
    public SubRadarChart subRaderChart;

    public void RebuildSubRadar(float edges)
    {
        subRaderChart.SetRadarVertext(new []{vert1, vert2, vert3, vert4, vert5}, edges);
        subRaderChart.SetVerticesDirty();
    }
}