using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scarecrow : MonoBehaviour, IHitable
{
    public bool IsEnemy => true;

    public Vector3 HitPoint { get => Vector3.zero; set => HitPoint = value; }

    public UnityEvent HitEvent;

    public void GetHit(float damage, GameObject damageDealer, Vector3 HitNormal)
    {
        Debug.Log(1);
        PoolManager.Instance.Pop("AttackEffect");
        HitEvent?.Invoke();
    }
}
