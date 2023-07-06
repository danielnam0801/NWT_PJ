using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBeeAnimator : EnemyAgentAnimator
{
    private readonly int _canChangeStateHash = Animator.StringToHash("isCanChange");

    public void SetChangeHash()
    {
        Animator.SetTrigger(_canChangeStateHash);
    }
}
