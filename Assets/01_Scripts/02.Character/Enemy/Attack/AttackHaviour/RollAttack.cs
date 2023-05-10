using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class RollAttack : EnemyAttack, INormalAttack
{
    [SerializeField] Transform CenterPos;
    [SerializeField] float rotateValue = 20f;
    [SerializeField] float _moveSpeed = 2f;

    public UnityEvent FaintStateEvent;
    public UnityEvent FaintStateEndEvent;
    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;

        StartCoroutine(Roll());
    }
    public float cRS;

    public void StopThis()
    {
        StopAllCoroutines();
    }

    IEnumerator Roll()
    {

        Vector3 pos = _brain.transform.position;
        //DOTween.Shake(() => _brain.transform.rotation.eulerAngles, z =>
        //{
        //    var rotation = _brain.transform.rotation;
        //    rotation.eulerAngles = Vector3.forward * z.z;
        //    _brain.transform.rotation = rotation;
        //}, 1f, 30, 8, 0);
        _brain.transform.DOJump(pos + new Vector3(0, 1, 0), 3, 1, 1f);

        yield return new WaitForSeconds(0.8f);
        AttackStartFeedback?.Invoke();

        float t = 0;
        float rollPlayTime = 2.5f;
        float rollStartAccerleration = 0.8f;
        float rollEndDecelerationTime = 0.8f;
        float currentRotateSpeed = 1f;
        int isLeft = (_brain.transform.position.x > _brain.Target.transform.position.x) ? 1 : -1; // 플레이어가 왼쪽에 있을때 true
        bool move = false;
        Vector2 dir = isLeft == 1 ? Vector2.left : Vector2.right;
        while (t < rollStartAccerleration + rollPlayTime + rollEndDecelerationTime || currentRotateSpeed >= 0f)
        {
            cRS = currentRotateSpeed;
            if (t < rollStartAccerleration + rollPlayTime && _stateInfo.IsCrash) // 돌고 있는데 충돌시 
            {
                Debug.Log("부딫힘ㄴ");
                _stateInfo.IsCrash = false;
                t = rollStartAccerleration + rollPlayTime; // 감속으로 넘어감
                break;
            }
            
            if (t < rollStartAccerleration) // 가속
            {
                if(move == false)
                {
                    move = true;
                }
                currentRotateSpeed += Time.deltaTime;
            }
            else if(rollStartAccerleration + rollPlayTime < t || t < rollEndDecelerationTime){ //감속
                currentRotateSpeed -= Time.deltaTime * 2;
                if (currentRotateSpeed <= 0f) break;
            }
            _brain.transform.RotateAround(CenterPos.position, isLeft * Vector3.forward, rotateValue * currentRotateSpeed * Time.deltaTime);
            _brain.Move(dir * currentRotateSpeed * _moveSpeed, _brain.Target.position);
            t += Time.deltaTime;
            yield return null;
        }

        float randomRotate = UnityEngine.Random.Range(-10f,5f);
        _brain.transform.DORotate(new Vector3(0, 0, -170f +randomRotate), 0.3f).SetEase(Ease.InOutBack);
        //_brain.transform.DOShakeRotation(0.2f);
        _brain.EnemyMovement.StopImmediatelly();
        _aiActionData.CreatePoint = this.transform.localPosition;
        
        AttackEndFeedback?.Invoke();
        FaintStateEvent?.Invoke();

        t = 0;
        float faintTime = 4f;
        bool isIn = false;
        while (t < faintTime) // 기절 상태
        {
            if (_stateInfo.IsHit)
            {
                isIn = true;
                FaintStateEndEvent?.Invoke();
                // 파티클 지우기
                break;
            }
            t+= Time.deltaTime;
            yield return null;
        }
        if(!isIn)
            FaintStateEndEvent?.Invoke();

        //기절끝나고 실행할 애니메이션
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
            _stateInfo.IsCrash = false;
            callBack();
        });

    }
}
