using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class testShadow : Singleton<testShadow>
{
    public Light[] DirectionalLight;
    //hexColor 값
    public string HexColor;

    //rgbColor 값
    public Color RgbColor;
    Color color;

    [Range(0,2)]
    public float Intensity;

    public UnityEvent OnDestroyAll = new UnityEvent();


    public void ShadowOn()
    {
        QualitySettings.shadows = ShadowQuality.All;
    }

    public void ShadowOff()
    {
        QualitySettings.shadows = ShadowQuality.Disable;
    }

    public void ChangeLightColor()
    {
        for (int i = 0; i < DirectionalLight.Length; i++)
        {
            ColorUtility.TryParseHtmlString(HexColor, out color);
            DirectionalLight[i].color = color;
            
            //DirectionalLight[i].color = RgbColor;//new Color(0,10,0);
        }
    }

    public void LightIntensity()
    {
        for (int i = 0; i < DirectionalLight.Length; i++)
        {
            DirectionalLight[i].intensity = Intensity;
        }
    }

    public void DestroyAllObjects()
    {
        OnDestroyAll.Invoke();
    }
}
