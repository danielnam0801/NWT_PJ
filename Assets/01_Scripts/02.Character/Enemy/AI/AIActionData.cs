using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionData : MonoBehaviour
{
    public bool IsCanThinking = true;
    public bool TargetSpotted;//Ÿ���� �߰��ߴ°�
    public bool Arrived;

    public bool IsIdle; 
    public bool IsMoving;

    public Vector3 CreatePoint;
    public Vector3 HitNormal;
    public SkillType currentSkill;
    public float attackWaitTime = 0;
}
