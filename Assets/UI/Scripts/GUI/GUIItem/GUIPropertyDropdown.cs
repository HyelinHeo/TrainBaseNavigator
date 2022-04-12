using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPropertyDropdown : GUIPropertyItem
{
    public Dropdown PropertyDropdown;

    public List<string> dropdownOptions;

    public override void Init()
    {
        base.Init();
        //FillData();
        OnChangeValue();
    }

    public void FillData()
    {
        PropertyDropdown.options.Clear();
        if (dropdownOptions.Count>0)
        {
            PropertyDropdown.captionText.text = dropdownOptions[0];
        }
        for (int i = 0; i < dropdownOptions.Count; i++)
        {
            AddData(dropdownOptions[i]);
        }
    }

    private void AddData(string data)
    {
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = data;
        PropertyDropdown.options.Add(option);
    }

    public void OnChangeValue()
    {
        ItemValue = PropertyDropdown.captionText.text;
    }
}
