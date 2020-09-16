using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngineInternal;


public class PlayerController : BaseController
{
    PlayerStat _stat;
    bool _stopSkill = false;

    float _Rot_Speed = 10.0f;
    float _y_Angle = 10.0f;

    int _mask = (1 << (int)Define.eLayer.Ground | (1 << (int)Define.eLayer.Monster));




    // Start is called before the first frame update
    public override void Init()
    {
        WorldObjectType = Define.eWorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();
        //Managers.input.KeyAction -= OnKey;
        //Managers.input.KeyAction += OnKey;

        Managers.input.MouseAction -= OnMouseEvent;
        Managers.input.MouseAction += OnMouseEvent;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    void OnHitEvent()
    {

        if(_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
        }

        if(_stopSkill)
        {
            State = Define.eState.Idle;
        }
        else
        {
            State = Define.eState.Skill;
        }
    }



    #region State Method

    protected override void UpdateSkill()
    {
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - this.transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20* Time.deltaTime);
        }
    }


    protected override void UpdateMoving()
    {
        if(_lockTarget != null)
        {
            float distance = (_destPos - this.transform.position).magnitude;
            if(distance <= 1)
            {
                State = Define.eState.Skill;
                return;
            }
        }

        Vector3 dir = _destPos - this.transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.1f)
        {
            State = Define.eState.Idle;
        }
        else
        {
            //NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            //float moveDist = Mathf.Clamp(_stat.MoveSpeed + Time.deltaTime, 0, dir.magnitude);
            //nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(this.transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if(Physics.Raycast(this.transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    State = Define.eState.Idle;

                return;
            }

            //transform.position += dir.normalized * moveDist;
            float moveDist = Mathf.Clamp(_stat.MoveSpeed + Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(dir), _Rot_Speed * Time.deltaTime);
        }


    }

    #endregion



    #region Key Callback

    void OnKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Vector3.forward), _Rot_Speed * Time.deltaTime);
            transform.position += (Vector3.forward * _stat.MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Vector3.back), _Rot_Speed * Time.deltaTime);
            //transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            transform.position += (Vector3.back * _stat.MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Vector3.left), _Rot_Speed * Time.deltaTime);
            //transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            transform.position += (Vector3.left * _stat.MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Vector3.right), _Rot_Speed * Time.deltaTime);
            //transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            transform.position += (Vector3.right * _stat.MoveSpeed * Time.deltaTime);
        }
    }


    void OnMouseEvent(Define.eMouseEvent _MouseEvent)
    {
        switch(State)
        {
            case Define.eState.Idle:
                {
                    OnMouseEvent_IdleRun(_MouseEvent);
                }
                break;

            case Define.eState.Moving:
                {
                    OnMouseEvent_IdleRun(_MouseEvent);
                }
                break;

            case Define.eState.Skill:
                {
                    if (_MouseEvent == Define.eMouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.eMouseEvent _MouseEvent)
    {
        //if (_MouseEvent != Define.eMouseEvent.Click)
        //    return;
        if (State == Define.eState.Die)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool braycasthit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        //int mask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Ground");

        switch(_MouseEvent)
        {
            case Define.eMouseEvent.PointerDown:
                {
                    if(braycasthit)
                    {
                        this._destPos = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
                        State = Define.eState.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.eLayer.Monster)
                        {
                            _lockTarget = hit.collider.gameObject;
                        }
                        else
                        {
                            _lockTarget = null;
                        }
                    }
                }
                break;

            case Define.eMouseEvent.Press:
                {
                    if(_lockTarget != null)
                    {
                        _destPos = _lockTarget.transform.position;
                    }
                    else
                    {
                        if(braycasthit)
                        {
                            _destPos = hit.point;
                        }
                    }
                }
                break;

            case Define.eMouseEvent.PointerUp:
                {
                    _stopSkill = true;
                }
                break;
        }

    }

    #endregion
}
