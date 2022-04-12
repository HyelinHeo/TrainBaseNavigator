using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GUIProperty : GUIWindow
{
    public Text Title;
    //부모가 되는 오브젝트
    public Transform Content;

    public CreatorManager CManager;

    public const string SUFFIX_TITLE = " Setting";

    /// <summary>
    /// 프리팹 리스트
    /// </summary>
    public GUIPropertyItem[] PropertyItems;

    /// <summary>
    /// 생성된 속성 아이템 리스트
    /// </summary>
    public List<GUIPropertyItem> CreatedItems;

    public UnityEvent OnItemControlMouseStart = new UnityEvent();
    public UnityEvent OnItemControlMouseEnd = new UnityEvent();


    private void Start()
    {
        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);
        FoundationModeManager.Inst.OnChangeMode.AddListener(o => OnChangeFoundationMode(o));
        OnChangeFoundationMode(FoundationMode.Plane);

        //CManager를 동적으로 넣기위한 하드코딩_20201016 수정필요
        CManager = GameObject.FindObjectOfType<CreatorManager>() as CreatorManager;

        Hide();

        SelectObject.AddSelectObjected.AddListener(SelectedObject);
    }

    private void OnChangeEditMode(EditMode mode)
    {
        Hide();
    }

    private void SelectedObject(SelectObject obj)
    {
        ClearItemList();
        SetProperties();

        Show();
    }


    List<SelectObject> selectObjects;
    void SetProperties()
    {
        selectObjects = SelectObject.SelectObjects;
        if (selectObjects != null)
        {
            for (int i = 0; i < selectObjects.Count; i++)
            {
                if (selectObjects[i] != null)
                {
                    MPXObject myObject = selectObjects[i].MyObject;
                    if (myObject != null)
                    {
                        MPXProperty[] properties = myObject.GetProperties();
                        if (properties != null)
                        {
                            ClearItemList();
                            ClearCreatedItems();
                            for (int k = 0; k < properties.Length; k++)
                            {
                                if (properties[k].UseUI)
                                {
                                    GUIPropertyItem item = InstantiateItem(properties[k]);
                                    item.OnValueChanged.AddListener(OnItemValueChanged);
                                    item.OnControlMouseStart.AddListener(OnControlMouseStart);
                                    item.OnControlMouseEnd.AddListener(OnControlMouseEnd);

                                    AddCreatedItem(item);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("SetProperties error. select object(mpxobject) not found. ");
                }
            }
        }
    }

    private void OnControlMouseEnd()
    {
        OnItemControlMouseEnd.Invoke();
    }

    private void OnControlMouseStart()
    {
        OnItemControlMouseStart.Invoke();
    }

    void ClearItemList()
    {
        if (CreatedItems != null)
        {
            for (int i = CreatedItems.Count - 1; i >= 0; i--)
            {
                CreatedItems[i].OnValueChanged.RemoveListener(OnItemValueChanged);
                CreatedItems[i].OnControlMouseStart.RemoveListener(OnControlMouseStart);
                CreatedItems[i].OnControlMouseEnd.RemoveListener(OnControlMouseEnd);
                Destroy(CreatedItems[i].gameObject);
            }
            CreatedItems = null;
        }
    }

    void AddCreatedItem(GUIPropertyItem item)
    {
        if (CreatedItems == null)
            CreatedItems = new List<GUIPropertyItem>();

        CreatedItems.Add(item);
    }

    void ClearCreatedItems()
    {
        if (CreatedItems != null)
        {
            CreatedItems.Clear();
        }
    }

    void SetPropertiesDefault(FoundationMode mode)
    {
        ClearItemList();
        if (mode == FoundationMode.ImportCad)
            return;

        CreatorProperty creator = CManager.GetCreator(mode);
        if (creator != null)
        {
            List<MPXProperty> properties = creator.Properties;
            if (properties != null)
            {
                int count = creator.PropertyCount;
                ClearCreatedItems();
                for (int i = 0; i < count; i++)
                {
                    if (properties[i].UseUI)
                    {
                        GUIPropertyItem item = InstantiateItem(properties[i]);
                        item.OnValueChanged.AddListener(OnItemValueChanged);

                        AddCreatedItem(item);
                    }
                }
            }
        }
    }

    private void OnItemValueChanged(GUIPropertyItem item)
    {
        selectObjects = SelectObject.SelectObjects;
        if (selectObjects != null)
        {
            for (int i = 0; i < selectObjects.Count; i++)
            {
                MPXObject obj = selectObjects[i].MyObject;
                obj.SetProperty(item.GetProperty());
                obj.Draw();
                obj.SelectObj.Refresh();
            }
        }
        else
        {
            CreatorProperty creator = CManager.GetCreator(FoundationModeManager.Inst.CurrentMode);
            if (creator != null)
            {
                creator.SetValue(item.GetProperty());
            }
        }
    }

    public override void Show()
    {
        //InstantiateItems
        base.Show();
    }

    private void OnChangeFoundationMode(FoundationMode mode)
    {
        SetTitle(mode.ToString());

        //CManager를 동적으로 넣기위한 하드코딩_20201016 수정필요
        if (CManager == null)
            CManager = GameObject.FindObjectOfType<CreatorManager>() as CreatorManager;
        SetPropertiesDefault(mode);
    }

    public void SetTitle(string title)
    {
        Title.text = title + SUFFIX_TITLE;
    }

    public override void Hide()
    {
        base.Hide();

        FoundationModeManager.Inst.ChangeMode(FoundationMode.NONE);
    }

    public override void Init()
    {
        base.Init();
    }

    public void OnClickCloseBtn()
    {
        Hide();
    }

    GUIPropertyItem InstantiateItem(MPXProperty p)
    {
        GUIPropertyItem obj = Instantiate(PropertyItems[(int)p.ItemType]);
        obj.transform.SetParent(Content);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.Init();
        obj.SetValue(p);

        obj.gameObject.SetActive(true);

        return obj;
    }



    private void OnDestroy()
    {
        FoundationModeManager.Inst.OnChangeMode.RemoveListener(o => OnChangeFoundationMode(o));
    }
}
