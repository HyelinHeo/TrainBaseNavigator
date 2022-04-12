using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWindow : GUIBase
{
    public int Level;

    /// <summary>
    /// color define
    /// </summary>
    public Color DefaultColor;
    public Color HighlightColor = Color.white;

    public override void Show()
    {
        base.Show();
        transform.SetAsLastSibling();
    }
}
