using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] float chargeTime;

    Collider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<Collider2D>();
    }
    private void Start()
    {
        circleCollider.offset = Vector2.zero;
        transform.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        Tween scaleUp = transform.DOScale(Vector3.one, 3f);
        seq.Append(scaleUp);
        Tween shake = transform.DOShakePosition();
    }
}
