using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHitDecision : AIDecision
{
    public override bool MakeADecision()
    {
        Debug.LogError("�� ���ݽ� �ǰ� �ɶ� ó�� ������Ԥ�");
        return !_aiStateInfo.IsAttack && _aiStateInfo.IsHit;
    }
}
