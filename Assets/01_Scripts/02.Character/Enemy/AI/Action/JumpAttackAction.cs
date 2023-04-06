using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackAction : AIAction
{
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField]
    private bool isJumping;
    public bool IsJumping => isJumping;
    public override void Init()
    {
        _brain.EnemyMovement.StopImmediatelly();
        isJumping = false;
    }

    public override void TakeAction()
    {
        if(isJumping == false)
        {
            Debug.Log("JUmp");
            isJumping=true;
            StartCoroutine("JumpTo");
        }
    }

    private IEnumerator JumpTo()
    {
        Debug.Log("Jumping");
   
        Vector2 start = _brain.transform.position;
        Vector2 end = _brain.Target.position;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end)) / _jumpSpeed;

        float currentTime = 0;
        float percent = 0;

        float v0 = (end - start).y - _gravity; // y방향의 최고점이고 이게 속도로 정의

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime; //이게 lerp 용 시간

            Vector3 pos = Vector3.Lerp(start, end, percent);
            // 포물선 운동 : 시작위치 + 초기속도 * 시간 + 중력 * 시간제곱
            pos.y = start.y + (v0 * percent) + (_gravity * percent * percent);
            _brain.transform.position = pos;

            yield return null;
        }

        isJumping = false;
    }
}
