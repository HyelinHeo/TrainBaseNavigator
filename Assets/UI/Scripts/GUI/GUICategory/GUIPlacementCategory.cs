using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// dropdown(category) index, item index
/// </summary>
public class GUIPlacementCategory : GUIWindow
{
    private int categoryIndex;
    public List<string> CategoryList;
    public Dropdown CategoryDropdown;
    public UnityEvent<int> OnCategoryChange = new UnityEvent<int>();

    public const int ERRORINDEX = -1;

    /// <summary>
    /// 초기화
    /// </summary>
    public override void Init()
    {
        CategoryList.Clear();
        CategoryList = null;
        CategoryList = new List<string>();
        categoryIndex = 0;


        VerDemoFillData();
    }

    /// <summary>
    /// 데모용_20201015_추후 삭제
    /// </summary>
    void VerDemoFillData()
    {
        CategoryList.Add("열차");
        CategoryList.Add("운송장비");
        CategoryList.Add("부품");
        FillData();
    }

    void FillData()
    {
        CategoryDropdown.options.Clear();
        if (CategoryList.Count > 0)
        {
            CategoryDropdown.captionText.text = CategoryList[0];
        }
        for (int i = 0; i < CategoryList.Count; i++)
        {
            AddData(CategoryList[i]);
        }
    }

    private void AddData(string data)
    {
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = data;
        CategoryDropdown.options.Add(option);
    }

    public virtual void SetIndex(int val)
    {
        categoryIndex = val;
    }

    public virtual int GetIndex(string categoryName)
    {
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = categoryName;
        for (int i = 0; i < CategoryDropdown.options.Count; i++)
        {
            if (CategoryDropdown.options[i].Equals(option.text))
            {
                categoryIndex = i;
                return categoryIndex;
            }
        }
        return ERRORINDEX;
    }

    public void OnValueChange()
    {
        categoryIndex = CategoryDropdown.value;
        OnCategoryChange.Invoke(categoryIndex);
        //Debug.Log(CategoryDropdown.value);
    }
}
