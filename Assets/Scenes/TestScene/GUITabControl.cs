using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUITabControl : GUIBase
{
    public Toggle ThisToggle;

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
        ChangeTabList();
    }

    public void ChangeTabList()
    {
        if (ThisToggle.isOn)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
