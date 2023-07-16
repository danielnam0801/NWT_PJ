using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : MonoBehaviour
{

    [SerializeField]
    private WeaponSO info;
    public float rotateTime = 1;
    public LayerMask WallLayer;

    protected Transform playerSwordTrm;
    public bool IsFollow { get; set; }
    public bool IsStay { get; set; }
    public bool IsAttack = false;
    public bool IsSkill = false;
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
    [SerializeField]
    private float circleAttackCount = 5;
    [SerializeField]
    private float circleSkillDamage;


    [Header("pentagon")]
    public UnityEvent ExplosionEvent;

    [Header("triangle")]
    [SerializeField]
    private int bounceCount = 3;
    [SerializeField]
    private float wallCheckDistance = 0.2f;
    [SerializeField]
    private float triangleMoveSpeed = 5f;
    [SerializeField]
    private float triangleMoveTime = 1f;

    [Header("star")]
    [SerializeField]
    private float starSkillRadius = 5f;
    [SerializeField]
    private float starSkillMoveSpeed = 10f;


    protected virtual void Awake()
    {
        IsFollow = true;
        IsStay = false;
        playerSwordTrm = GameObject.Find("Player/SwordPosition").transform;
    }

    protected virtual void Start()
    {
        LightManager.Instance.AddFocusObject(gameObject);

        circleSkillDamage = info.power / 3f;
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsAttack || IsSkill)
            return;

        if (collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hit))
        {
            hit.GetHit(Info.power, gameObject,
                (collision.bounds.center - transform.position).normalized);
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

    #region 공격
    public void Attack(List<Vector2> pathPoints, ShapeType _type)
    {
        StartCoroutine(AttackCoroutine(pathPoints, _type));
    }

    private IEnumerator AttackCoroutine(List<Vector2> pathPoints, ShapeType _type)
    {
        IsAttack = true;
        IsFollow = false;
        transform.position = pathPoints[0];

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

        yield return StartCoroutine(ChooseAttackSkill(_type));
        IsAttack = false;
        StartCoroutine("Stay");
    }

    public void RangeAttack(float radius, float damage, Action action = null)
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, radius);

        if (col.Length > 0)
        {
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].TryGetComponent<IHitable>(out IHitable obj))
                {
                    action?.Invoke();
                    obj.GetHit(damage, gameObject,
                        (col[i].transform.position - transform.position).normalized);
                }
            }
        }
    }

    private IEnumerator ChooseAttackSkill(ShapeType shape)
    {
        IsSkill = true;

        switch (shape)
        {
            case ShapeType.Circle:
                yield return StartCoroutine(CircleSkill());
                break;
            case ShapeType.Pentagon:
                yield return null;
                PentagonSkill();
                break;
            case ShapeType.Triangle:
                yield return StartCoroutine(TriangleSkill());
                break;
            case ShapeType.Default:
                break;
            case ShapeType.FourStar:
                yield return StartCoroutine(StarShapeSkill(4));
                break;
            case ShapeType.FiveStar:
                yield return StartCoroutine(StarShapeSkill(5));
                break;
            case ShapeType.SixStar:
                yield return StartCoroutine(StarShapeSkill(6));
                break;
        }       


        IsSkill = false;
    }
    #endregion

    #region 스킬
    private IEnumerator CircleSkill()
    {
        Debug.Log("start cir");
        float current = 0f;
        float percent = 0f;
        float attackTime = circleSkillTime / circleAttackCount;
        float currentAttackTime = 0;

        Vector2 dir = transform.up;
        float angle = Mathf.Atan2(dir.y, dir.x);

        while (percent < 1)
        {
            current += Time.deltaTime;
            currentAttackTime += Time.deltaTime;

            percent = current / circleSkillTime;
            angle -= Time.deltaTime * 30;

            if(currentAttackTime - attackTime >= 0)
            {
                RangeAttack(1.5f, circleSkillDamage);
                currentAttackTime = 0;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg), percent);

            yield return null;
        }
    }

    private IEnumerator TriangleSkill()
    {
        int currnetBounceCount = 0;
        //Vector2 dir = (Quaternion.Euler(0, 0, 45f) * transform.up).normalized;
        Vector2 dir = Vector2.zero;
        RaycastHit2D hit;
        float currentMoveTime = 0;

        yield return new WaitUntil(() => TriangleSkillWaitClick(out dir));

        while(currnetBounceCount <= bounceCount)
        {
            currentMoveTime += Time.deltaTime;

            if (currentMoveTime >= triangleMoveTime)
                break;

            Debug.Log("tri");
            transform.position += (Vector3)(dir * triangleMoveSpeed * Time.deltaTime);

            hit = Physics2D.Raycast(transform.position, dir, wallCheckDistance, WallLayer);

            if(hit)
            {
                Vector2 reflectVec = Vector2.Reflect(dir, hit.normal).normalized;
                dir = reflectVec;
                currnetBounceCount++;
                currentMoveTime = 0;

                if(currnetBounceCount <= bounceCount)
                {
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, angle + 45 - 180);
                }
            }

            yield return null;
        }
    }

    private bool TriangleSkillWaitClick(out Vector2 dir)
    {
        dir = Vector2.zero;

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = (mousePos - (Vector2)transform.position).normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 45 - 180);

            return true;
        }

        return false;
    }

    private void PentagonSkill()
    {
        RangeAttack(2, 5, () => ExplosionEvent?.Invoke());
    }

    private IEnumerator StarShapeSkill(int point)
    {
        int count;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, starSkillRadius, 1 << 8);
        Debug.Log(cols.Length);
        count = cols.Length > point ? point : cols.Length;

        for(int i = 0; i < count; i++)
        {
            Vector2 startPos = transform.position;
            Vector2 endPos = cols[i].transform.Find("GuidePosition").position;
            Rotate(endPos);
            float distance = Vector2.Distance(startPos, endPos);
            float time = distance / starSkillMoveSpeed;
             
            yield return StartCoroutine(LerpMove(startPos, endPos, time));

            if (cols[i].gameObject.TryGetComponent<IHitable>(out IHitable hit))
            {
                hit.GetHit(Info.power, gameObject,
                    (cols[i].transform.position - transform.position).normalized);
                Debug.Log(cols[i].name);
            }
            //
        }
    }
    #endregion

    private IEnumerator LerpMove(Vector2 start, Vector2 end, float time)
    {
        float current = 0;  

        while(current < time)
        {
            transform.position = Vector2.Lerp(start, end, current / time);
            current += Time.deltaTime;

            yield return null;
        }
    }

    #region 대기, 회전
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
    #endregion
}
