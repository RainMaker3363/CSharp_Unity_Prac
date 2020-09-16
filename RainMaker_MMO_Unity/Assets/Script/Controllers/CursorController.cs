using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int _mask = (1 << (int)Define.eLayer.Ground | (1 << (int)Define.eLayer.Monster));
    public enum eCursorType
    {
        None,
        Attack,
        Hand,
    }

    eCursorType _cursorType = eCursorType.None;
    Texture2D AttackIconCursor;
    Texture2D HandIconCursor;

    // Start is called before the first frame update
    void Start()
    {
        AttackIconCursor = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        HandIconCursor = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.eLayer.Monster)
            {
                if (_cursorType == eCursorType.Attack)
                    return;

                _cursorType = eCursorType.Attack;
                Cursor.SetCursor(AttackIconCursor, new Vector2(AttackIconCursor.width / 5, 0.0f), CursorMode.Auto);
            }
            else
            {
                if (_cursorType == eCursorType.Hand)
                    return;

                _cursorType = eCursorType.Hand;
                Cursor.SetCursor(HandIconCursor, new Vector2(HandIconCursor.width / 3, 0.0f), CursorMode.Auto);
            }
        }
    }
}
