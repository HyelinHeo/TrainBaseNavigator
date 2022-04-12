using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlayTime : GUIPlayWindow
{
    public Text TextPlayTime;
    public string TimeMeasure;

    public GameObject Target;

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
        SetTextInit();
    }

    public override void SetOffset()
    {
        this.transform.localPosition = Offset;
    }

    public void InitOffset()
    {
        this.transform.localPosition = Vector3.zero;
    }
 
    public void SetText(float val)
    {
        TextPlayTime.text = string.Format("{0:N2}{1}", val, TimeMeasure);
    }

    public void SetTextInit()
    {
        SetText(0);
    }

    public string GetText()
    {
        return TextPlayTime.text;
    }

    private void Update()
    {
        if (IsShow && Target != null)
        {
            transform.position = MPXCamera.CurrentCamera.WorldToScreen(Target.transform.position);
        }
    }
}
