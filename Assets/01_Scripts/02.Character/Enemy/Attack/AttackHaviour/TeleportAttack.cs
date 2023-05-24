using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class TeleportAttack : EnemyAttack, IMeleeAttack
{
    [SerializeField] ParticleSystem portal;
    public UnityEvent<Vector3> FlashEffect;

    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        Teleport();
        _animator.OnAnimaitionEventTrigger += AnimEvent;
        _animator.Animator.SetFloat("AttackSpeed", 1);
    }

    private void AnimEvent()
    {
        Vector3 pos = _animator.transform.position;
        FlashEffect?.Invoke(pos);
        _animator.Animator.SetFloat("AttackSpeed", 0);
        _brain.transform.localScale = Vector3.zero;
        _brain.Enemy.SetGravityScale(0f);

    }

    private void ReAppear()
    {
        ChangeEnemyPos();
        Vector3 pos = _animator.transform.position;
        FlashEffect?.Invoke(pos);
        _brain.transform.localScale = Vector3.one;
        _animator.Animator.SetFloat("AttackSpeed", 1);
        _brain.Enemy.SetGravityScale(1f);
    }

    float randomX;
    Vector3 moveNextPos;

    private void Teleport()
    {
        float appearTime = 1f;
        ParticleSystem portal1 = Instantiate(portal, _animator.transform.position + new Vector3(0,1.5f, 0), Quaternion.identity);
        portal1.transform.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(ResizePortal(portal1, 1f, 1f));
        seq.AppendInterval(appearTime);
        seq.Append(ResizePortal(portal1, 0f, 1f));
        seq.Append(MySequence2(appearTime));
        seq.OnComplete(() =>
        {
            _brain.transform.localScale = Vector3.one;
            _animator.OnAnimaitionEventTrigger -= AnimEvent;
            CallbackPlay();
        });
    }

    private void ChangeEnemyPos()
    {
        _brain.transform.position = moveNextPos;
    }

    Sequence MySequence2(float appearTime)
    {
        SetTarget();
        //ChangeEnemyPos();
        ParticleSystem portal2 = Instantiate(portal, moveNextPos, Quaternion.identity);
        portal2.transform.localScale = Vector3.zero;
        return DOTween.Sequence().Append(ResizePortal(portal2, 1f, 1f)).AppendCallback(() => ReAppear())
            .AppendInterval(appearTime).Append(ResizePortal(portal2, 0f, 1f));
    }
    private Tween ResizePortal(ParticleSystem portal, float value, float duration) => portal.transform.DOScale(value, duration).SetEase(Ease.OutBounce);
    private void SetTarget()
    {
        randomX = _brain.Target.position.x + UnityEngine.Random.Range(-1f, 1f);
        moveNextPos = new Vector3(randomX, _animator.transform.position.y + 1.5f, _animator.transform.position.z);
    }
}
