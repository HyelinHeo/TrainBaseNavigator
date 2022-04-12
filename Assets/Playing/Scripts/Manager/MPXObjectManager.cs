using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPXObjectManager : MonoBehaviour
{
    public CreatorManager FoundationCreators;
    public CreatorPutObject PlacementCreator;

    private GUILoading loading;

    private WaitForSeconds wait = new WaitForSeconds(0.5f);

    void Start()
    {
        loading = GUIManager.Inst.Get<GUILoading>();
    }

    CreatorProperty GetCreator(EditMode mode, FoundationMode fMode)
    {
        switch (mode)
        {
            case EditMode.DEFAULT:
                break;
            case EditMode.FOUNDATION:
                return FoundationCreators.GetCreator(fMode);
            case EditMode.PLACEMENT:
                return PlacementCreator;
            case EditMode.NAVIGATION:
                break;
            default:
                break;
        }

        return null;
    }

    public MPXObject[] GetObjectAll()
    {
        return GetComponentsInChildren<MPXObject>();
    }

    public void NewWork()
    {
        FoundationCreators.InitNew();
        PlacementCreator.InitNew();
    }

    public IEnumerator LoadWork()
    {
        XMLSaveFile xml = XMLSaveFileManager.Inst.Xml;

        if (xml != null)
        {
            int count = xml.Count;
            if (count > 0)
            {
                GUIManager.Inst.Get<GUIPlacement>().Show();

                List<XMLMPXObject> xmlObjs = xml.Objects;
                for (int i = 0; i < count; i++)
                {
                    XMLMPXObject xmlObj = xmlObjs[i];
                    CreatorProperty creator = GetCreator(xmlObj.EditType, xmlObj.FoundationType);

                    creator.Init();
                    if (xmlObj.EditType == EditMode.PLACEMENT)
                    {
                        MPXObject prefab = GUIManager.Inst.Get<GUIPlacement>().Items.FindPlacementItemObject(xmlObj.PrefabName);
                        creator.Prefab = prefab;
                    }

                    MPXObject loadObj = creator.Create(xmlObj.Properties);

                    yield return null;

                    loadObj.transform.position = xmlObj.Position;
                    loadObj.transform.eulerAngles = xmlObj.Rotation;
                    loadObj.transform.localScale = xmlObj.Size;
                    loadObj.name = xmlObj.Name;
                    loadObj.ID = xmlObj.ID;

                    creator.Init();

                    loading.SetProgressValue((float)i / count);
                }

                loading.SetProgressValue(1f);
                GUIManager.Inst.Get<GUIPlacement>().Hide();
            }
        }

        yield return wait;
    }
}
