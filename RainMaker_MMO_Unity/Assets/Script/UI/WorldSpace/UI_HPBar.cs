using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum eGameObjects
    {
        HPBar
    }

    Stat _stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(eGameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = this.gameObject.transform.parent;
        transform.position = parent.position + (Vector3.up * (parent.GetComponent<Collider>().bounds.size.y));
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHPRation(ratio);
    }

    public void SetHPRation(float ratio)
    {
        GetObject((int)eGameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
