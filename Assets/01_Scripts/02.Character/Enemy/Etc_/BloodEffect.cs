using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : PoolableObject
{
    public override void Init()
    {
        
    }

    public void Push()
    {
        PoolManager.Instance.Push(this);
    }
}
