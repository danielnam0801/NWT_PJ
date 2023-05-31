using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashCheck : MonoBehaviour
{
    [SerializeField] LayerMask CrashAble; // 충돌 가능한 것들
    AIStateInfo _aiStateInfo;
    private void Awake()
    {
        _aiStateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == CrashAble)
        {
            Debug.Log("IsCrashAble");
            _aiStateInfo.IsCrash = true;
        }
    }
}
