using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 可以让这个不可见的控件压根不参与绘制！让它彻底的消失，但是还能够阻挡后面ui的操作
/// </summary>
public class IgnoreRaycast : MaskableGraphic 
{
    protected IgnoreRaycast()
    {
        useLegacyMeshGeneration = false;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
}
