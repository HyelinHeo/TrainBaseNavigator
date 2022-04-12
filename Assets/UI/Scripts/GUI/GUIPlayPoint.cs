using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlayPoint : GUIPlayWindow
{
    public Text TextPlayPoint;
    public int PlayPointIndex;

    public GameObject Target;

  
    public override void Show()
    {
        base.Show();
        MyAnim.Play("SpotlightPoint");
    }

    public override void Hide()
    {
        base.Hide();
        MyAnim.Stop("SpotlightPoint");
    }

    public override void Init()
    {
        base.Init();
    }

    public override void SetOffset()
    {
        this.transform.localPosition = Offset;
    }

    public void InitOffset()
    {
        this.transform.localPosition = Vector3.zero;
    }

    public void SetText(string txt)
    {
        TextPlayPoint.text = txt;
    }

    public void SetTextStart()
    {
        SetText("S");
        PlayPointIndex = 0;
    }

    public string GetText()
    {
        return TextPlayPoint.text;
    }

    public void SetIndex(int idx)
    {
        PlayPointIndex = idx;
        SetText(idx.ToString());
    }

    public int GetIndex()
    {
        return PlayPointIndex;
    }

    private void Update()
    {
        if (IsShow && Target != null)
        {
            transform.position = MPXCamera.CurrentCamera.WorldToScreen(Target.transform.position);
        }
    }
}
