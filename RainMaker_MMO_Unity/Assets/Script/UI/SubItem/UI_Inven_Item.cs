using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{

    enum eGameObjects
    {
        ItemIcon,
        ItemNameText
    }

    string _name;

    public override void Init()
    {
        Bind<GameObject>(typeof(eGameObjects));

        Get<GameObject>((int)eGameObjects.ItemNameText).GetComponent<Text>().text = _name;
        Get<GameObject>((int)eGameObjects.ItemIcon).BindEvent((PointerEventData) =>
        {
            Debug.Log($"아이템 클릭 : {_name}");
        });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
