using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] float chargeTime;
    [SerializeField] float strength = 0.3f;
    [SerializeField] int vibrato = 10;
    [SerializeField] int randomness = 90;
    [SerializeField] float shakeTime = 2f;

    float shootPower = 5f;
    float damage;
    Transform target;

    public LayerMask CanHittable;

    Collider2D circleCollider;
    Rigidbody2D rb;

    private void Awake()
    {
        circleCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        SetValueAndPlay(3, transform);
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

    IEnumerator Shooting()
    {
        while (gameObject.activeSelf)
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
            rb.velocity = transform.up * shootPower;
            yield return new WaitForSeconds(0.04f);
        }
    }

    public void SetValueAndPlay(float dmamage, Transform targetPos)
    {
        this.damage = dmamage;
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
