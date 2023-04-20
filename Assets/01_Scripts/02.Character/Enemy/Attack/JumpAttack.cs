using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class JumpAttack : EnemyAttack
{
    [SerializeField]
    private int _bezeirResolution = 30;
    private Vector3[] _bezierPoints;

    [SerializeField]
    private float _jumpSpeed = 0.9f, _jumpDelay = 0.4f, _impactRadius = 2f;
    //점프가 다 완료되는데 걸리는 시간과 점프를 시작하기전까지 걸리는 딜레이
    private float _frameSpeed = 0; //점프 프레임당 걸리는 시간

    [SerializeField]
    private bool isJumping;
    public bool IsJumping => isJumping;
    private Action callBack = null;

    Rigidbody2D rb2d;

    public UnityEvent PlayLandingAnimation; //착지 애니메이션 재생

    protected override void Awake()
    {
        base.Awake();
        rb2d = _brain.transform.GetComponent<Rigidbody2D>();
    }

    public override void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        JumpAct();
    }

    void JumpAct()
    {
        rb2d.gravityScale = 0f;
        //Debug.Log("JumpStart");
        Jump();
    }

    private void Jump()
    {
        Vector3 deltaPos = transform.position - _brain.BasePos.position;
        Vector3 targetPos = _brain.GetTargetUnderPosition() + deltaPos; //점프 지점
        Vector3 startControl = (targetPos - transform.position) / 4;
        //Debug.Log("DeltaPos + " + deltaPos);
        //Debug.Log("targetPOs + " + targetPos);

        float angle = targetPos.x - transform.position.x < 0 ? -45f : 45f;

        Vector3 cp1 = Quaternion.Euler(0, 0, angle) * startControl;
        Vector3 cp2 = Quaternion.Euler(0, 0, angle) * (startControl * 4); 


        _bezierPoints = DOCurve.CubicBezier.GetSegmentPointCloud(transform.position,
            transform.position + cp1, targetPos, transform.position + cp2, _bezeirResolution);
        _frameSpeed = _jumpSpeed / _bezeirResolution;

        //Debug.Log(_frameSpeed);
        StartCoroutine(JumpCoroutine());

        //디버그용 코드들
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = _bezierPoints.Length;
        lr.SetPositions(_bezierPoints);

    }

    IEnumerator JumpCoroutine()
    {
        AttackFeedBack?.Invoke(); //공격사운드 재생후 0.4초후 점프
        yield return new WaitForSeconds(_jumpDelay);
        
        for (int i = 0; i < _bezierPoints.Length; i++)
        {
            yield return new WaitForSeconds(_frameSpeed);
            _brain.transform.position = _bezierPoints[i];
            if (i == _bezierPoints.Length - 5)  //종료 5프레임 전이면 랜딩 애니메이션 재생
            {
                EdgeOfEndAnimation();
            }
        }
        JumpEnd();
        Debug.Log("JumpEnd");
    }

    //점프가 거의 끝나갈때쯤 재생할 애니메이션
    private void EdgeOfEndAnimation()
    {
        PlayLandingAnimation?.Invoke();
    }

    //점프가 끝나는 시점에서 호출될 코드
    public void JumpEnd()
    {
        rb2d.gravityScale = 9.8f;
        //ImpactScript impact = PoolManager.Instance.Pop("ImpactShockwave") as ImpactScript;
        Vector3 basePos = _brain.BasePos.position; // 발바닥을 중심으로 충격파 발생

        float randomRot = UnityEngine.Random.Range(0, 360f);
        Quaternion rot = Quaternion.Euler(0, 0, randomRot);

        //impact.SetPositionAndRotation(basePos, rot);

        Vector3 dir = _brain.Target.position - basePos;

        if (dir.sqrMagnitude <= _impactRadius * _impactRadius) //반경내에 들어왔다면
        {
            IHitable targetHit = _brain.Target.GetComponent<IHitable>();
            //targetHit?.GetHit(_brain.Enemy.EnemyData.Damage, gameObject);

            //if (dir.sqrMagnitude == 0)
            //{
            //    dir = UnityEngine.Random.insideUnitCircle;
            //}
            //IKnockBack targetKnockback = GetTarget().GetComponent<IKnockBack>();
            //targetKnockback?.KnockBack(dir.normalized, 5f, 1f);
        }
        //DelayCoroutine(AfterAttackDelayTime, callBack);
        StartCoroutine(DelayCoroutine(AfterAttackDelayTime, callBack));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
