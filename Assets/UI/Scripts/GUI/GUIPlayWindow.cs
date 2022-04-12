using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// gui play window는 show hide기능에서 animation이 다르므로 show 와 hide는 부모의 것을 받지 않는다.
/// </summary>
public class GUIPlayWindow : GUIBase
{
    /// <summary>
    /// +양수값 -음수값
    /// </summary>
    public Vector2 Offset;

    public override void Show()
    {
        if (MYPanel != null)
        {
            MYPanel.SetActive(true);
        }
    }

    public override void Hide()
    {
        if (MYPanel != null)
        {
            MYPanel.SetActive(false);
        }
    }

    public override void Init()
    {
        base.Init();
    }

    public virtual void SetOffset()
    {

    }
}
