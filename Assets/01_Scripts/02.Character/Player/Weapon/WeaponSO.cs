using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public float power;
    public float stayTime;
    [Tooltip("DrawLien ���� pathPointInterval��ū �̵��ϴ� �ð�")]
    public float pointUnitMoveTime;
    public float followSpeed;
    public float followMinDistance;
    public float attackDelayTime;
}
