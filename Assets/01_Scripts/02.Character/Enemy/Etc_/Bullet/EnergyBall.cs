using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class EnergyBall : PoolableObject
{
    [SerializeField] float chargeTime;
    [SerializeField] float strength = 0.3f;
    [SerializeField] int vibrato = 10;
    [SerializeField] int randomness = 90;
    [SerializeField] float shakeTime = 2f;
    [SerializeField]
    float shootPower = 10f;

    [SerializeField] GameObject ballBombEffect;

    int passbyPlayerCnt = 0;
    private float currentShootPower;
    float damage;

    bool isCanCounting = true;
    Transform target;

    Collider2D circleCollider;
    Rigidbody2D rb;
    public bool isShootReady = false;

    private Vector3 scale;
    public Action DestroyEvent;

    private void Awake()
    {
        circleCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        currentShootPower = shootPower;
        scale = transform.localScale;
    }

    public void SetValueAndPlay(float dmamage, Transform targetPos)
    {
        DestroyEvent += () =>
        {
            Instantiate(ballBombEffect, transform.position, Quaternion.identity);
        };
        this.damage = dmamage;
        this.target = targetPos;
        circleCollider.enabled = false;
        circleCollider.offset = Vector2.zero;
        transform.localScale = Vector3.zero;
        DGShoot();
    }
    private void DGShoot()
    {
        Sequence seq = DOTween.Sequence();
        Tween scaleUp = transform.DOScale(scale, chargeTime);
        seq.Append(scaleUp).AppendCallback(() => {
            circleCollider.enabled = true;
            isShootReady = true;
        });
        Tween shake = transform.DOShakePosition(shakeTime, strength, vibrato, randomness).SetEase(Ease.InExpo);
        seq.Insert(chargeTime - 1.5f, shake).AppendCallback(Shoot);
    }

    private void Shoot()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        rb.velocity = dir * currentShootPower;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90, Vector3.forward);
        StartCoroutine(nameof(Shooting));
    }


    private float delayTime = 0.06f;
    private void ChangeDelayTime(float time) => delayTime = time; 
    
    IEnumerator DelayCoroutine(float changeTime, float normalTime, float delayTime)
    {
        float delay = delayTime / 2;
        canSetTarget = true;
        SetSpeed(false, delay);
        yield return new WaitForSeconds(delay - 0.05f);
        SetSpeed(true, delay);
        yield return new WaitForSeconds(delay);

        isCanCounting = true;
        //ChangeDelayTime(normalTime);
    }

    public void SetSpeed(bool isIncrease, float playTime)
    {
        if (!isIncrease)
        {
            DOVirtual.Float(currentShootPower, 2f, playTime, (t) => currentShootPower = t).SetEase(Ease.InQuad);
        }
        else
        {
            DOVirtual.Float(currentShootPower, shootPower, playTime, (t) => currentShootPower = t).SetEase(Ease.InBack);
        }
    }

    private bool canSetTarget = true;
    public float CanChaseMaxCnt = 3;
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(0.5f);
        while (gameObject.activeSelf)
        {
            if (Vector2.Distance(transform.position, target.position) < 5f && isCanCounting == true)
            {
                //canSetTarget = false;
                passbyPlayerCnt++;
                if(passbyPlayerCnt < CanChaseMaxCnt)
                {
                    float eventPlayTime = 1.7f;
                    StartCoroutine(DelayCoroutine(0.1f, 0.05f, eventPlayTime));
                }
                isCanCounting = false;
                yield return null;
            }

            if(passbyPlayerCnt < CanChaseMaxCnt && canSetTarget == true)
            {
                Vector3 dir = (target.position - transform.position).normalized;
                float dot = Vector3.Dot(transform.up, dir);
                if (dot < 1.0f)
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    Vector3 cross = Vector3.Cross(transform.up, dir);
                    if (cross.z < 0)
                    {
                        angle = transform.rotation.eulerAngles.z - Mathf.Min(10, angle);
                    }
                    else
                    {
                        angle = transform.rotation.eulerAngles.z + Mathf.Min(10, angle);
                    }
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                rb.velocity = transform.up * currentShootPower;
            }

            if (passbyPlayerCnt == CanChaseMaxCnt)
            {
                yield return new WaitForSeconds(1f);
                PoolManager.Instance.Push(this);
            }

            yield return new WaitForSeconds(delayTime);
        }
    }

    private void OnDisable()
    {
        DestroyEvent?.Invoke();
    }

    public override void Init()
    {
        currentShootPower = shootPower;
        scale = transform.localScale;
        passbyPlayerCnt = 0;
        isShootReady = false;
        isCanCounting = true;
        delayTime = 0.06f;
        canSetTarget = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PoolManager.Instance.Push(this);
    }
}



