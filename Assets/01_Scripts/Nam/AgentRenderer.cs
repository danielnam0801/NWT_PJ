using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AgentRenderer : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    AIMovementData _movementData;
    AIActionData _actionData;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _actionData = transform.parent.Find("AI").GetComponent<AIActionData>();
    }

    public void ChaseAttackFaceDirection(Vector2 pointerInput)
    {
        if (_actionData.isAttack == false)
        {
            Vector3 direction = (Vector3)pointerInput - transform.position;
            transform.localScale = (direction.x > 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }

    public void IdleFaceDirection(Vector2 currentDir, Vector2 beforeDir)
    {
        if (currentDir.x == 0)
            transform.localScale = (beforeDir.x > 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //멈춤이 일어날때 전에 보던 방향에 따른 페이스 디렉션
        else
            transform.localScale = (currentDir.x > 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

    }
}
