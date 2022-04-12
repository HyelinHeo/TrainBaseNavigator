using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMode : GUIWindow
{
    public GUIFoundation GuiFoundation;
    public GUIPlacement GuiPlacement;
    public GUINavigation GuiNavigation;
    public Image FoundationImg;
    public Image PlacementImg;
    public Image SimulationImg;
    public Text TxtManageBtn;
    Button btnFileSave;
    Button btnFileExit;



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
        EditModeManager.Inst.OnChangeMode.AddListener(o => OnChangeMode(o));
        btnFileSave = GUIManager.Inst.Get<GUIMenu>().FileSave;
        btnFileExit = GUIManager.Inst.Get<GUIMenu>().FileExit;
    }

    public override void ChangeState()
    {
        if (IsShow)
        {
            TxtManageBtn.text = "＞";
        }
        else
        {
            TxtManageBtn.text = "＜";
        }
        base.ChangeState();
    }

    private void OnChangeMode(EditMode mode)
    {
        switch (mode)
        {
            case EditMode.FOUNDATION:
                GuiFoundation.Show();
                GuiPlacement.Hide();
                GuiNavigation.Hide();
                FoundationImg.color = HighlightColor;
                PlacementImg.color = DefaultColor;
                SimulationImg.color = DefaultColor;

                btnFileSave.interactable = true;
                break;

            case EditMode.PLACEMENT:
                GuiFoundation.Hide();
                GuiPlacement.Show();
                GuiNavigation.Hide();
                FoundationImg.color = DefaultColor;
                PlacementImg.color = HighlightColor;
                SimulationImg.color = DefaultColor;

                btnFileSave.interactable = true;
                break;

            case EditMode.NAVIGATION:
                GuiFoundation.Hide();
                GuiPlacement.Hide();
                GuiNavigation.Show();
                FoundationImg.color = DefaultColor;
                PlacementImg.color = DefaultColor;
                SimulationImg.color = HighlightColor;
                
                btnFileSave.interactable = false;
                break;

            default:
                break;
        }
    }

    private void OnDestroy()
    {
        EditModeManager.Inst.OnChangeMode.RemoveListener(o => OnChangeMode(o));
    }
}
