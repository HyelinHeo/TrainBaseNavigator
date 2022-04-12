using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPointer : MonoBehaviour
{
    public PlayObject PlayObj;
    public GUIPlayPoint GUIPointer;
    public GUIDrawLine GUIArrow;

    [SerializeField]
    private GUIPlayPoint myPointer;

    [SerializeField]
    private GUIDrawLine myArrow;


    void Start()
    {
        GUIPointer = GUIManager.Inst.Get<GUINavigation>().GuiPlayPoint;
        GUIArrow = GUIManager.Inst.Get<GUIDrawLine>();

        if (PlayObj.UnSelect != null)
            PlayObj.UnSelect.AddListener(UnSelected);
    }

 
    private void UnSelected()
    {
        Debug.Log("Unselected. " + gameObject);
        HidePointer();
    }

    public void ShowPointer(int idx)
    {
        if (myPointer == null)
        {
            myPointer = InstantiateGUI(GUIPointer);

            myPointer.Target = PlayObj.MyObject.gameObject;
        }
        myPointer.SetIndex(idx);
        myPointer.Show();
    }

    public void Show(int idx, MPXObject start, MPXObject end)
    {
        ShowPointer(idx);
        ShowArrow(start, end);
    }


    public void Hide()
    {
        HidePointer();
        HideArrow();
    }

    public void ShowArrow(MPXObject start, MPXObject end)
    {
        if (myArrow == null)
        {
            myArrow = InstantiateGUI(GUIArrow);

        }
        myArrow.start = start;
        myArrow.end = end;
        myArrow.Show();
    }

    public void HideArrow()
    {
        if (myArrow != null)
        {
            myArrow.Hide();
        }
    }

    public void HidePointer()
    {
        if (myPointer != null)
        {
            myPointer.Hide();
        }
    }

    public void Clear()
    {
        if (myPointer != null)
            Destroy(myPointer.gameObject);

        myPointer = null;
    }

    public T InstantiateGUI<T>(T ori) where T : GUIPlayWindow
    {
        T p = Instantiate(ori);
        p.transform.SetParent(GUIManager.Inst.Get<GUIPlaying>().PlayingNavigation.transform);
        p.transform.localScale = Vector3.one;
        p.transform.localPosition = Vector3.zero;
        return p;
    }

    public void ShowStartPointer()
    {
        if (myPointer == null)
        {
            myPointer = InstantiateGUI(GUIPointer);

            myPointer.Target = PlayObj.MyObject.gameObject;
        }
        myPointer.SetTextStart();
        myPointer.Show();
    }
}
