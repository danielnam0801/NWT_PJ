using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public float power;
    public float stayTime;
    [Tooltip("DrawLien 변수 pathPointInterval만큰 이동하는 시간")]
    public float pointUnitMoveTime;
    public float followSpeed;
    public float followMinDistance;
    public float attackDelayTime;
}
