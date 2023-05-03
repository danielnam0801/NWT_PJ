using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RollAttack : EnemyAttack
{
    [SerializeField] Transform CenterPos;
    [SerializeField] float rotateValue = 20f;
    public override void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        AttackStartFeedback?.Invoke();

        StartCoroutine(Roll());
    }
    public float cRS;
    IEnumerator Roll()
    {
        AttackStartFeedback?.Invoke();

        Vector3 pos = _brain.transform.position;
        _brain.transform.DOJump(pos + new Vector3(0, 1, 0), 3, 1, 1f);

        yield return new WaitForSeconds(0.7f);

        float t = 0;
        float rollPlayTime = 2.3f;
        float rollStartAccerleration = 1.5f;
        float rollEndDecelerationTime = 1f;
        float currentRotateSpeed = 0.5f;
        int isRight = (_brain.transform.position.x > _brain.Target.transform.position.x) ? 1 : -1; // 플레이어가 오른쪽에 있을때
        while (t < rollStartAccerleration + rollPlayTime + rollEndDecelerationTime || currentRotateSpeed >= 0f)
        {
            cRS = currentRotateSpeed;
            if (_stateInfo.IsCrash) // 돌고 있는데 충돌시 
            {
                _stateInfo.IsCrash = false;
                t = rollStartAccerleration + rollPlayTime; // 감속으로 넘어감
                break;
            }
            
            if (t < rollStartAccerleration) // 가속
            {
                currentRotateSpeed += Time.deltaTime;
            }
            else if(rollStartAccerleration + rollPlayTime < t || t < rollEndDecelerationTime){ //감속
                currentRotateSpeed -= Time.deltaTime;
                if (currentRotateSpeed <= 0f) break;
            }
            _brain.transform.RotateAround(CenterPos.position, isRight * Vector3.forward, rotateValue * currentRotateSpeed * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
        Sequence sequence = DOTween.Sequence();
        Tween tween1 = _brain.transform.DOShakeRotation(0.2f);
        Tween tween2 =
            _brain.transform.DOJump(_brain.transform.position + new Vector3(0, 0.2f, 0), 1, 0, 0.1f);
        Tween tween3 =
            _brain.transform.DORotate(new Vector3(0, 0, 0), 0.15f);

        sequence.Append(tween1);
        sequence.Append(tween2);
        sequence.Join(tween3);
        sequence.OnComplete(() =>
        {
            AttackEndFeedback?.Invoke();
            callBack();
        });

    }
}
