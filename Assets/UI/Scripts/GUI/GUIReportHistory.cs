using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIReportHistory : GUIReportItem
{
    public Text TxtHistorySubTitle;

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

    public void SetText(string historyName, float val)
    {
        TxtHistorySubTitle.text = historyName;
        TxtValue.text = string.Format("{0:N2}{1}", val, TimeMeasure);
    }

    
}
