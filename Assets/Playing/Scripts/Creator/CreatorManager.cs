using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorManager : MonoBehaviour
{
    public CreatorProperty[] Creators;


    void Start()
    {
        
    }

    public void InitNew()
    {
        for (int i = 0; i < Creators.Length; i++)
        {
            Creators[i].Init();
            Creators[i].InitNew();
        }
    }

    public CreatorProperty GetCreator(FoundationMode mode)
    {
        return Creators[(int)mode];
    }

    public void Load()
    {
   
       
    }
}
