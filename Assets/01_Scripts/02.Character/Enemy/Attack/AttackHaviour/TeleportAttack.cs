using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeleportAttack : EnemyAttack, IMeleeAttack
{
    [SerializeField] ParticleSystem portal;

    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        _animator.OnAnimaitionEventTrigger += Teleport;
        _animator.OnAnimaitionEndTrigger += AnimationEnd;
    }

    private void AnimationEnd()
    {
        _animator.OnAnimaitionEventTrigger -= Teleport;
        _animator.OnAnimaitionEndTrigger -= AnimationEnd;
        CallbackPlay();
    }

    private void Teleport()
    {
        StartCoroutine(PortalAppear());
    }

    private IEnumerator PortalAppear()
    {
        float maxT = portal.main.duration;
        float t = 0;
        Sequence seq = DOTween.Sequence();
        Tween ResizePortal = transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
        seq.Append(ResizePortal);
        seq.AppendInterval(maxT - 2f);
        seq.Append(ResizePortal);
    }
    private Tween ResizePortal(float value, float duration) => transform.DOScale(value, duration).SetEase(Ease.OutBounce);  
}
