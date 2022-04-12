using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIReportTotalTime : GUIReportItem
{
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
    }

    public void SetText(float val)
    {
        TxtValue.text = string.Format("{0:N2}{1}", val, TimeMeasure);
    }

    public string GetText()
    {
        return TxtValue.text;
    }
}
