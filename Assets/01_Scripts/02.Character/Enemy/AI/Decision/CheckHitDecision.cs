using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHitDecision : AIDecision
{
    public override bool MakeADecision()
    {
        Debug.LogError("적 공격시 피격 될때 처리 해줘야함ㄴ");
        return !_aiStateInfo.IsAttack && _aiStateInfo.IsHit;
    }
}
