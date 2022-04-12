using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIFile : GUIWindow
{ 
    public void NewWorkspace()
    {
        PrefsManager.SetString(PrefsManager.WORK_FILE_NAME, PrefsManager.WROK_FILE_NEW);
        PrefsManager.Save();

        SceneManager.LoadScene(1);
    }

    public void LoadWorkspace()
    {
        SceneManager.LoadScene(1);
    }

    public override void Hide()
    {
        Show();
    }

    public void LoadWorkSpaceDemo()
    {
        PrefsManager.SetString(PrefsManager.WORK_FILE_NAME, "sample");
        PrefsManager.Save();

        SceneManager.LoadScene(1);
    }

}
