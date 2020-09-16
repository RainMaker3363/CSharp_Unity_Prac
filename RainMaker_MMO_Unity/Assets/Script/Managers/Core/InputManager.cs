using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.eMouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0.0f;

    public void OnUpdate()
    {
        //  UI 클릭여부 확인
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey == true && KeyAction != null)
            KeyAction.Invoke();

        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                if(!_pressed)
                {
                    MouseAction.Invoke(Define.eMouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }

                MouseAction.Invoke(Define.eMouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if(Time.time < _pressedTime + 0.2f)
                        MouseAction.Invoke(Define.eMouseEvent.Click);
                    
                    MouseAction.Invoke(Define.eMouseEvent.PointerUp);
                }
                    

                _pressed = false;
                _pressedTime = 0;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
