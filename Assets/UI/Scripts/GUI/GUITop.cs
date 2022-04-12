using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITop : GUIBase
{
    public GUIMenu GuiMenu;
    public GUIMode GuiMode;
    //public GUIUtility GuiUtil;

    public override void Init()
    {
        GuiMenu = GUIManager.Inst.Get<GUIMenu>();
        GuiMode = GUIManager.Inst.Get<GUIMode>();

        GuiMenu.Show();
        GuiMode.Show();
        //GuiUtil.Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (!IsShow)
                Show();
            else
                Hide();

        }

    }

    public override void Show()
    {
        base.Show();

        GuiMenu.Show();
        GuiMode.Show();
    }

    public override void Hide()
    {
        base.Hide();

        GuiMenu.Hide();
        GuiMode.Hide();
    }
}
