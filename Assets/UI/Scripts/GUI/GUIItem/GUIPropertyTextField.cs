using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPropertyTextField : GUIPropertyItem
{
    //인풋 필드 입력 칸
    public InputField TxtInputField;

    public override void Init()
    {
        base.Init();
        OnChangeValue();
    }

    public void OnChangeValue()
    {
        ItemValue = TxtInputField.text;
    }

}
