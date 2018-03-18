using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubRadarChart : Graphic
{
    private VertexHelper vertextHelper;

    [SerializeField] private Color bottomColor;

    [SerializeField] private Color topColor;

    //用于记录背景多边形的顶点数组位置
    private Vector2[] vertPos = new Vector2[7];

    //用于记录多边形每个顶点的颜色
    private Color[] vertColor = new Color[6];

    //多边形类型
    public ShadeType shadeType;

    /*更加传入的顶点数据来绘制雷达图
        顶点位置示意图
    
                 2  
                 |
                 |
       3         |         5
                 6
                 |
                 |
                 |
            0    1     4
            
            
    
     * 顶点颜色
     *                3
     * 
     * 
     * 
     *     4                       2
     *                 
     *
     *
     *
     *
     *           0             1
     *
     */
    public void SetRadarVertext(float[] vertArray, float edges)
    {
        if (vertArray.Length == 5)
        {
            float length = edges / 2 / Mathf.Cos(Mathf.Deg2Rad * 54);
            //计算各个顶点的位置
            Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
            Vector4 vector4 = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + edges,
                pixelAdjustedRect.y + edges);

            //初始化顶点和颜色数组

            //计算顶点和颜色数组
            //0
            vertPos[0] = new Vector2(vector4.x + (1 - vertArray[0]) * edges / 2,
                vector4.y + Mathf.Tan(Mathf.Deg2Rad * 54) * (1 - vertArray[0]) * edges / 2);
            //1
            vertPos[1] = new Vector2(vector4.x + edges / 2, vector4.y);
            //2
            vertPos[2] = new Vector2(vector4.x + edges / 2,
                vector4.y + length * (Mathf.Sin(Mathf.Deg2Rad * 54) + vertArray[3]));
            //3
            vertPos[3] = new Vector2(vector4.x + edges / 2 - length * vertArray[4] * Mathf.Cos(Mathf.Deg2Rad * 18),
                vector4.y + length * Mathf.Sin(Mathf.Deg2Rad * 54) +
                length * Mathf.Sin(Mathf.Deg2Rad * 18) * vertArray[4]);
            //4
            vertPos[4] = new Vector2(vector4.x + edges / 2 * (1 + vertArray[1]),
                vector4.y + length * Mathf.Sin(Mathf.Deg2Rad * 54) * (1 - vertArray[1]));
            //5
            vertPos[5] = new Vector2(vector4.x + edges / 2 + length * vertArray[2] * Mathf.Cos(Mathf.Deg2Rad * 18),
                vector4.y + length * Mathf.Sin(Mathf.Deg2Rad * 54) +
                length * Mathf.Sin(Mathf.Deg2Rad * 18) * vertArray[2]);
            //6
            vertPos[6] = new Vector2(vector4.x + edges / 2, vector4.y + edges / 2 * Mathf.Tan(Mathf.Deg2Rad * 54));


            vertColor[0] = Color.Lerp(bottomColor, topColor,
                Mathf.Sin(54 * Mathf.Deg2Rad) * (1 - vertArray[0]) / (Mathf.Sin(54 * Mathf.Deg2Rad) + 1));
            vertColor[1] = Color.Lerp(bottomColor, topColor,
                Mathf.Sin(54 * Mathf.Deg2Rad) * (1 - vertArray[1]) / (Mathf.Sin(54 * Mathf.Deg2Rad) + 1));
            vertColor[2] = Color.Lerp(bottomColor, topColor,
                (Mathf.Sin(54 * Mathf.Deg2Rad) + vertArray[2] * Mathf.Cos(72 * Mathf.Deg2Rad)) /
                (Mathf.Sin(54 * Mathf.Deg2Rad) + 1));
            vertColor[3] = Color.Lerp(bottomColor, topColor,
                (Mathf.Sin(54 * Mathf.Deg2Rad) + vertArray[3]) / (Mathf.Sin(54 * Mathf.Deg2Rad) + 1));
            vertColor[4] = Color.Lerp(bottomColor, topColor,
                (Mathf.Sin(54 * Mathf.Deg2Rad) + vertArray[4] * Mathf.Cos(72 * Mathf.Deg2Rad)) /
                (Mathf.Sin(54 * Mathf.Deg2Rad) + 1));
            vertColor[5] = Color.Lerp(bottomColor, topColor,
                Mathf.Sin(54 * Mathf.Deg2Rad) / (Mathf.Sin(54 * Mathf.Deg2Rad) + 1));
        }
        else
        {
            //TODO 传入的数据没有按照格式传递
            throw new FormatException();
        }
    }

    //重绘UI的Mesh
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (shadeType == ShadeType.Pentagon)
        {
            DrawPentagonByCenter(vh);
            Debug.Log("重绘制子雷达图");
        }
    }
    
    /*通过中心点绘制五边形
     * 顶点位置示意图
     *                3
     * 
     * 
     * 
     *     4                       2
     *                 5
     *
     *
     *
     *
     *           0             1
     *
     */

    private void DrawPentagonByCenter(VertexHelper vh)
    {
        Color32 color = (Color32) this.color;
        vh.Clear();
        //添加左半边的四边形
        //0
        vh.AddVert(new Vector3(vertPos[0].x, vertPos[0].y), vertColor[0], new Vector2(0.0f, 0.0f));
        //1
        vh.AddVert(new Vector3(vertPos[4].x, vertPos[4].y), vertColor[1], new Vector2(1.0f, 0.0f));
        //2
        vh.AddVert(new Vector3(vertPos[5].x, vertPos[5].y), vertColor[2], new Vector2(1f, 0.5f));
        //3
        vh.AddVert(new Vector3(vertPos[2].x, vertPos[2].y), vertColor[3], new Vector2(0.5f, 1f));
        //4
        vh.AddVert(new Vector3(vertPos[3].x, vertPos[3].y), vertColor[4], new Vector2(0.0f, 0.5f));
        //5
        vh.AddVert(new Vector3(vertPos[6].x, vertPos[6].y), vertColor[5], new Vector2(0.0f, 0.5f));

        //添加左半边的三角形
        vh.AddTriangle(0, 1, 5);
        vh.AddTriangle(5, 1, 2);
        vh.AddTriangle(5, 2, 3);
        vh.AddTriangle(5, 3, 4);
        vh.AddTriangle(5, 4, 0);
    }
}