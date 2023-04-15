using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkAction : AIAction
{
    [SerializeField] float thinkTime = 5f; // +- 1씩 랜덤으로 해줄껀
    [SerializeField] float idleThinkTime = 3f; // +- 1씩 랜덤으로 해줄꺼 (Idle상태일때 
    public override void Init()
    {
        _brain.OnMovementKeyPress?.Invoke(Vector2.zero);
    }

    public override void TakeAction()
    {
        if (_aiActionData.isCanThinking)
        {
            _aiMovementData.direction = new Vector2(NextMove(), 0);
            _aiActionData.isCanThinking = false;
        }
    }
    public int NextMove()
    {

        int nextMove = UnityEngine.Random.Range(-1, 2);

        _aiMovementData.thinkTime = UnityEngine.Random.Range(this.thinkTime-1f, this.thinkTime + 1f);

        if (nextMove == 0) _aiMovementData.thinkTime -= UnityEngine.Random.Range(this.idleThinkTime -0.5f, this.idleThinkTime + 0.5f);

        StartCoroutine("EnemyThink", _aiMovementData.thinkTime);
        return nextMove;
    }

    IEnumerator EnemyThink(float thinkTime)
    {
        yield return new WaitForSeconds(thinkTime);
        _aiActionData.isCanThinking = true;
        _aiMovementData.beforeDirection = new Vector2(_aiMovementData.direction.x, _aiMovementData.direction.y);
    }
}
