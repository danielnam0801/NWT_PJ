using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class JumpAttack : EnemyAttack, INormalAttack
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

    Rigidbody2D rb2d;

    public UnityEvent PlayLandingAnimation; //착지 애니메이션 재생

    protected override void Awake()
    {
        base.Awake();
        rb2d = _brain.transform.GetComponent<Rigidbody2D>();
    }

    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        _animator.OnAnimaitionEventTrigger += OnAnimEventAction;
    }

    public void OnAnimEventAction()
    {
        rb2d.gravityScale = 0f;
        //Debug.Log("JumpStart");
        Jump();

    }

    private void Jump()
    {
        Vector3 deltaPos = transform.position - _brain.BasePos.position;
        Vector3 targetPos = _brain.GetTargetUnderPosition(); //점프 지점
        Vector3 startControl = (targetPos - transform.position) / 4;

        float angle = targetPos.x - transform.position.x < 0 ? -45f : 45f;

        Vector3 cp1 = Quaternion.Euler(0, 0, angle) * startControl;
        Vector3 cp2 = Quaternion.Euler(0, 0, angle) * (startControl * 4); 


        _bezierPoints = DOCurve.CubicBezier.GetSegmentPointCloud(transform.position,
            transform.position + cp1, targetPos, transform.position + cp2, _bezeirResolution);
        _frameSpeed = _jumpSpeed / _bezeirResolution;

        //Debug.Log(_frameSpeed);
        StartCoroutine(JumpCoroutine());

        //디버그용
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = _bezierPoints.Length;
        lr.SetPositions(_bezierPoints);

    }

    float _changeFrameSpeed;
    IEnumerator JumpCoroutine()
    {
        _changeFrameSpeed = _frameSpeed;
        AttackStartFeedback?.Invoke(); //공격사운드 재생후 0.4초후 점프
        yield return new WaitForSeconds(_jumpDelay + 0.1f);
        
        for (int i = 0; i < _bezierPoints.Length; i++)
        {
            //if (i < _bezeirResolution / 5) _changeFrameSpeed = _frameSpeed;
            //else if (i >= _bezeirResolution / 5 && i < _bezeirResolution / 10 * 7) _changeFrameSpeed = _frameSpeed /2;
            //else _changeFrameSpeed = _frameSpeed;

            yield return new WaitForSeconds(_changeFrameSpeed);
            _brain.transform.position = _bezierPoints[i];
        }
        JumpEnd();
        Debug.Log("JumpEnd");
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
        StartCoroutine(DelayCoroutine(0.3f, () =>
        {
            Debug.Log("ISATTackINs");
            callBack?.Invoke();
            _animator.OnAnimaitionEventTrigger -= OnAnimEventAction;
            //AttackEndFeedback?.Invoke();
        }));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
