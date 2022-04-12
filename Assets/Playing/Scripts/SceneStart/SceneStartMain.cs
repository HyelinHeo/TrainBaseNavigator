using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneStartMain : SceneStart<SceneStartMain>
{
    public MPXObjectManager ObjManager;
    private GUILoading loading;


    public UnityEvent OnStartProcessComplete = new UnityEvent();

    private void Awake()
    {
        XMLSaveFileManager.Inst.OnLoadFailed.AddListener(OnLoadFileFailed);
    }

    private void OnLoadFileFailed()
    {
        ObjManager.NewWork();
    }

    private IEnumerator Start()
    {
        loading = GUIManager.Inst.Get<GUILoading>();
        loading.Show();

        string fileName = PrefsManager.GetString(PrefsManager.WORK_FILE_NAME, PrefsManager.WROK_FILE_NEW);
     
        XMLSaveFileManager.Inst.FileName = fileName;

        if (fileName == PrefsManager.WROK_FILE_NEW)
        {
            //todo
            XMLSaveFileManager.Inst.NewFileName();
            ObjManager.NewWork();

        }
        else
        {
            yield return StartCoroutine(XMLSaveFileManager.Inst.LoadFile());
            yield return StartCoroutine(ObjManager.LoadWork());
        }


        EditModeManager.Inst.ChangeMode(EditMode.PLACEMENT);

        loading.Hide();

        OnStartProcessComplete.Invoke();
    }

}
