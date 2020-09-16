using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum eGameObjects
    {
        GridPanel,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(eGameObjects));

        GameObject gridPanel = Get<GameObject>((int)eGameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        
        for(int i = 0; i<8; ++i)
        {

            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            
            var InvenItem = item.GetOrAddComponent<UI_Inven_Item>();
            InvenItem?.SetInfo($"집행검{i}번");
        }
    }
}
