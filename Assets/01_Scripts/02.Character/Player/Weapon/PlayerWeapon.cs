using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : MonoBehaviour
{

    [SerializeField]
    private WeaponSO info;
    public float rotateTime = 1;

    protected Transform playerSwordTrm;
    public bool IsFollow { get; set; }
    public bool IsStay { get; set; }
    private bool isAttack = false;
    public WeaponSO Info
    {
        get { return info; }
        private set { }
    }

    [Space]
    [Header("skill")]

    [Header("circle")]
    [SerializeField]
    private float radius = 2f;
    [SerializeField]
    private float targetAngleOffset = 1080f;
    [SerializeField]
    private float circleSkillTime = 1f;

    public UnityEvent ExplosionEvent;

    protected virtual void Awake()
    {
        IsFollow = true;
        IsStay = false;
        playerSwordTrm = GameObject.Find("Player/SwordPosition").transform;
    }

    protected virtual void Start()
    {
        LightManager.Instance.AddFocusObject(gameObject);
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttack)
            return;
        if (collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hit))
        {
            hit.GetHit(info.power, gameObject);
            Debug.Log(collision.name);
        }
    }

    private void MoveToPlayer()
    {
        if (!IsFollow)
            return;

        if(Vector2.Distance(transform.position, playerSwordTrm.position) >= info.followMinDistance)
        {
            transform.position = Vector2.Lerp(transform.position, playerSwordTrm.position, Time.deltaTime * info.followSpeed);
        }
    }

    public void Attack(List<Vector2> pathPoints, ShapeType _type)
    {
        StartCoroutine(AttackCoroutine(pathPoints, _type));
    }

    private IEnumerator AttackCoroutine(List<Vector2> pathPoints, ShapeType _type)
    {
        isAttack = true;
        IsFollow = false;
        transform.position = pathPoints[0];
        Debug.Log(pathPoints.Count);

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            float currentMoveTime = 0;

            Rotate(pathPoints[i + 1]);

            while(currentMoveTime < 1)
            {
                currentMoveTime += Time.deltaTime / info.pointUnitMoveTime;
                
                transform.position = Vector2.Lerp(pathPoints[i], pathPoints[i + 1], currentMoveTime);

                yield return null;
            }
        }
        yield return StartCoroutine(CircleSkill());
        //yield return StartCoroutine(ChooseAttackSkill(_type));
        isAttack = false;
        StartCoroutine("Stay");
    }

    private IEnumerator ChooseAttackSkill(ShapeType shape)
    {
        switch(shape)
        {
            case ShapeType.Circle:
                yield return StartCoroutine(CircleSkill());
                break;
            case ShapeType.Pentagon:
                yield return null;
                PentagonSkill();
                break;
            default:
                yield break;
        }
    }

    private IEnumerator CircleSkill()
    {
        Debug.Log("start cir");
        float current = 0f;
        float percent = 0f;
        Quaternion start = transform.rotation;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / circleSkillTime;

            transform.rotation = Quaternion.Lerp(start, Quaternion.Euler(0, 0, start.z + targetAngleOffset), percent);

            yield return null;
        }
    }

    private void PentagonSkill()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 2f);

        if (col.Length > 0)
        {
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].TryGetComponent<IHitable>(out IHitable obj))
                {
                    ExplosionEvent?.Invoke();
                    obj.GetHit(5, gameObject);
                }
            }
        }

        Debug.Log("Pentagon skill");
    }

    private IEnumerator Stay()
    {
        Debug.Log("stay");
        IsStay = true;
        IsFollow = false;

        yield return new WaitForSeconds(info.stayTime);

        IsStay = false;
        IsFollow = true;
        DrawManager.Instance.SetDelayDraw(Info.attackDelayTime);
        StartCoroutine(RotateVertical());
    }

    public void StopStay()
    {
        StopCoroutine("Stay");
        IsStay = false;
        IsFollow = true;
        StartCoroutine(RotateVertical());
    }

    private void Rotate(Vector2 targetDir)
    {
        Vector2 dir = (targetDir - (Vector2)transform.position).normalized;

        transform.right = Quaternion.Euler(0, 0, -135f) * dir;
    }

    private IEnumerator RotateVertical()
    {
        Vector2 originRight = transform.right; 
        Vector2 dir = Vector2.up;

        float current = 0;
        float percent = 0;
        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / rotateTime;

            transform.right = Vector2.Lerp(originRight, Quaternion.Euler(0, 0, -135f) * dir, percent);
            yield return null;  
        }
    }
}
