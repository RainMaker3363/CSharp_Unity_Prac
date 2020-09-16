using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    enum eButtons
    {
        PointButton
    }

    enum eTexts
    {
        PointText,
        ScoreText
    }

    enum eGameObjects
    {
        TestObject,
    }

    enum eImages
    {
        ItemIcon
    }

    int _Score = 0;


    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(eButtons));
        Bind<Text>(typeof(eTexts));
        Bind<GameObject>(typeof(eGameObjects));

        Bind<Image>(typeof(eImages));



        var go = GetImage((int)eImages.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) =>
        {
            go.transform.position = data.position;
        }, Define.UIEvent.Drag);

        GetButton((int)eButtons.PointButton).gameObject.BindEvent(OnButtonClicked);
    }


    public void OnButtonClicked(PointerEventData data)
    {
        ++_Score;

        Get<Text>((int)eTexts.ScoreText).text = $"점수 : {_Score}";
    }
}
