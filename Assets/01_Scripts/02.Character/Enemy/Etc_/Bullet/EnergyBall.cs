using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] float chargeTime;
    [SerializeField] float strength = 0.3f;
    [SerializeField] int vibrato = 10;
    [SerializeField] int randomness = 90;
    [SerializeField] float shakeTime = 2f;
    [SerializeField]
    float shootPower = 5f;

    int passbyPlayerCnt = 0;
    private float currentShootPower;
    float damage;

    bool isCanCounting = true;
    Transform target;

    public LayerMask CanHittable;

    Collider2D circleCollider;
    Rigidbody2D rb;

    private void Awake()
    {
        circleCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        currentShootPower = shootPower;
    }

    private void Start()
    {
        SetValueAndPlay(3, target);
    }

    private void DGShoot()
    {
        Sequence seq = DOTween.Sequence();
        Tween scaleUp = transform.DOScale(Vector3.one, chargeTime);
        seq.Append(scaleUp).AppendCallback(() => circleCollider.enabled = true);
        Tween shake = transform.DOShakePosition(shakeTime, strength, vibrato, randomness).SetEase(Ease.InExpo);
        seq.Insert(chargeTime - 1f, shake).AppendCallback(Shoot);
    }

    private void Shoot()
    {
        StartCoroutine(nameof(Shooting));
    }


    private float delayTime = 0.05f;
    private void ChangeDelayTime(float time) => delayTime = time; 
    
    IEnumerator DelayCoroutine(float changeTime, float normalTime, float delayTime)
    {
        Debug.Log("IsIN");
        ChangeDelayTime(changeTime);

        float delay = delayTime / 2;

        SetSpeed(false, delay);
        yield return new WaitForSeconds(delay);
        SetSpeed(true, delay);
        yield return new WaitForSeconds(delay);

        isCanCounting = true;
        ChangeDelayTime(normalTime);
    }

    public void SetSpeed(bool isIncrease, float playTime)
    {
        if (!isIncrease)
        {
            DOVirtual.Float(currentShootPower, 1f, playTime, (t) => currentShootPower = t).SetEase(Ease.InQuad);
        }
        else
        {
            DOVirtual.Float(currentShootPower, shootPower, playTime, (t) => currentShootPower = t).SetEase(Ease.InBack);
        }
    }


    IEnumerator Shooting()
    {
        delayTime = 0.05f;
        while (gameObject.activeSelf)
        {

            if (Vector2.Distance(transform.position, target.position) < 2f && isCanCounting == true)
            {
                passbyPlayerCnt++;
                if(passbyPlayerCnt <= 1)
                {
                    float eventPlayTime = 2f;
                    StartCoroutine(DelayCoroutine(0.1f, 0.05f, eventPlayTime));
                }
                isCanCounting = false;
                yield return null;
            }

            if(passbyPlayerCnt < 2)
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
            }
            rb.velocity = transform.up * currentShootPower;
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void SetValueAndPlay(float dmamage, Transform targetPos)
    {
        this.damage = dmamage;
        this.target = targetPos;
        circleCollider.enabled = false;
        circleCollider.offset = Vector2.zero;
        transform.localScale = Vector3.zero;
        DGShoot();
    }

    private void DestroyEvent()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.IsTouchingLayers(CanHittable))
        {
            IHitable hittable;
            if(collision.gameObject.TryGetComponent(out hittable))
            {
                hittable.GetHit(damage, damageDealer: this.gameObject);
                DestroyEvent();
            }
        }
    }
}
