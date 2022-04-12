using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIMenu : GUIWindow
{
    //public GameObject PanelStart;
    public GameObject PanelPlay;
    public Button FileSave;
    public Button FileExit;
    //public Image BtnFoundation;
    //public Image BtnPlacement;
    //public Image BtnSimulation;

    GUIMessageBox msgBox;

    public override void Init()
    {
        base.Init();

        msgBox = GUIManager.Inst.Get<GUIMessageBox>();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Save()
    {
        if (XMLSaveFileManager.Inst.Save())
        {
            msgBox.Show(GUIMessageBox.MessageType.Info, "저장이 완료 되었습니다", GUIMessageBox.MessageButtonType.OK);
        } else
        {
            msgBox.Show(GUIMessageBox.MessageType.Error, "저장 실패", GUIMessageBox.MessageButtonType.OK);
        }
    }
}
