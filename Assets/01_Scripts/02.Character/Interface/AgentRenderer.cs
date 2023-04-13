using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{

    AIMovementData _movementData;
    AIStateInfo _aiStateInfo;

    private void Awake()
    {
        _aiStateInfo = transform.parent.Find("AI").GetComponent<AIStateInfo>();;
    }

    public void ChaseAttackFaceDirection(Vector2 pointerInput)
    {
        if (_aiStateInfo.IsAttack == false)
        {
            Vector3 direction = (Vector3)pointerInput - transform.position;
            transform.parent.localScale = (direction.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }

    public void IdleFaceDirection(Vector2 currentDir, Vector2 beforeDir)
    {
        if (currentDir.x == 0)
            transform.parent.localScale = (beforeDir.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //멈춤이 일어날때 전에 보던 방향에 따른 페이스 디렉션
        else
            transform.parent.localScale = (currentDir.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

    }
}
