using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable 
{
    public bool IsEnemy { get; }
    public Vector3 HitPoint { get; set; }
    public void GetHit(float damage, GameObject damageDealer, Vector3 HitNormal);
}
