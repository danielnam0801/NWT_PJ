using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBossSpriteChange : MonoBehaviour
{
    Transform target;
    AIStateInfo stateInfo;

    private void Start()
    {
        target = GameManager.instance.Target;
        stateInfo = transform.parent.Find("AI").GetComponent<AIStateInfo>();
    }

    private void Update()
    {
        if (!stateInfo.IsAttack && !stateInfo.IsHit)
        {
            Vector2 dir = target.position - transform.position;
            transform.parent.localScale = (dir.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }
}
