using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GUIManager : Singleton<GUIManager>
{
    private Dictionary<string, GUIBase> gui_hashList = new Dictionary<string, GUIBase>();
    private readonly string PATH = "UI/";


    void Awake()
    {
        //DontDestroyOnLoad(this);

        Init();
    }

    public void Init()
    {
        //Add<GUIMenu>();
        //Add<GUIUtility>();
        //Add<GUITop>();
        Add<GUILoading>();
        Add<GUIMessageBox>();

        Add<GUIMenu>();
        Add<GUIMode>();

        Add<GUIPlacement>();
        Add<GUIFoundation>();
        Add<GUINavigation>();
        Add<GUIProperty>();

        Add<GUIPlaying>();
        Add<GUIDrawLine>();

        Add<GUIChangeView>();
        Add<GUIReport>();
    }

    public void Add<T>() where T : GUIBase, new()
    {
        string strUI = typeof(T).ToString();

        GUIBase item;
        if (!gui_hashList.TryGetValue(strUI, out item))
        {
            item = GameObject.FindObjectOfType<T>();
            if (item == null)
            {
                GameObject obj = Instantiate(Resources.Load(PATH + strUI)) as GameObject;

                if (obj == null)
                {
                    Debug.Log(PATH + strUI);
                }
                obj.transform.SetParent(this.transform);
                obj.transform.localScale = Vector3.one;
                obj.transform.localPosition = Vector3.zero;
                RectTransform rectTr = obj.GetComponent<RectTransform>();
                rectTr.offsetMax = Vector2.zero;
                rectTr.offsetMin = Vector2.zero;
                //Debug.Log(strUI + " : " + rectTr.offsetMax.x);
                //Debug.Log(strUI + " : " + rectTr.offsetMax.y);
                //Debug.Log(strUI + " : " + rectTr.offsetMin.x);
                //Debug.Log(strUI + " : " + rectTr.offsetMin.y);
                obj.name = strUI;
                item = obj.GetComponent<T>();
            }
            item.Init();
            gui_hashList.Add(strUI, item);
        }
    }

    public T Get<T>() where T : GUIBase
    {
        string strUI = typeof(T).ToString();
        GUIBase guiBase = null;
        if (false == gui_hashList.TryGetValue(strUI, out guiBase))
        {
            Debug.LogError("GUIManager :: Dont registration this Class" + strUI + "");
        }
        return (T)guiBase;
    }

    public void Release()
    {
        gui_hashList.Clear();
    }


    public void Remove(string strName)
    {

        GUIBase guiBase = null;
        if (false == gui_hashList.TryGetValue(strName, out guiBase))
        {
            Debug.LogError("GUIManager :: already Not exist UI" + strName);
        }
        gui_hashList.Remove(strName);
        GameObject.Destroy(guiBase.gameObject);
    }
}

