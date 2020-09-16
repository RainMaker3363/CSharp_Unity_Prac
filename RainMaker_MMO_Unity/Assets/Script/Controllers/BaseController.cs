using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 위치 벡터
// 방향 벡터
struct MyVector
{
    float x;
    float y;
    float z;

    public MyVector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public float magnitude
    {
        get
        {
            return Mathf.Sqrt(x * x + y * y + z * z);
        }
    }
    public MyVector normalized
    {
        get
        {
            return new MyVector(x / magnitude, y / magnitude, z / magnitude);
        }
    }

    public static MyVector operator +(MyVector a, MyVector b)
    {
        return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static MyVector operator -(MyVector a, MyVector b)
    {
        return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static MyVector operator *(MyVector a, float d)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }

    public static MyVector operator *(float d, MyVector a)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }
}



public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _destPos;

    [SerializeField]
    protected Define.eState _state = Define.eState.Idle;

    [SerializeField]
    protected GameObject _lockTarget;

    public Define.eWorldObject WorldObjectType { get; protected set; } = Define.eWorldObject.UnKnown;

    public virtual Define.eState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.eState.Idle:
                    {
                        anim.CrossFade("WAIT", 0.1f);
                    }
                    break;

                case Define.eState.Moving:
                    {
                        anim.CrossFade("RUN", 0.1f);
                    }
                    break;

                case Define.eState.Skill:
                    {
                        anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    }
                    break;

                case Define.eState.Die:
                    {

                    }
                    break;
            }
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (State)
        {
            case Define.eState.Moving:
                {
                    UpdateMoving();
                }
                break;

            case Define.eState.Idle:
                {
                    UpdateIdle();
                }
                break;

            case Define.eState.Die:
                {
                    UpdateDie();
                }
                break;

            case Define.eState.Skill:
                {
                    UpdateSkill();
                }
                break;
        }
    }

    public abstract void Init();

    protected virtual void UpdateDie()
    {

    }

    protected virtual void UpdateIdle()
    {

    }

    protected virtual void UpdateMoving()
    {

    }

    protected virtual void UpdateSkill()
    {

    }
}
