using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFeedback : Feedback
{
    public Transform playTrm;

    private PoolableObject effect;
    public override void CreateFeedBack()
    {
        effect = PoolManager.Instance.Pop("BloodEffect");
        effect.transform.position = playTrm.position;
        effect.transform.right = transform.parent.right;
    }

    public override void FinishFeedBack()
    {
        
    }
}
