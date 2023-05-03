using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionInner : AIDecision
{
    [SerializeField] Transform RayPoint;
    [SerializeField]
    [Range(0.1f, 30f)] private float _distance = 5f;
    [SerializeField] bool boxCastUse = true;
    public float Distance { get => _distance; set => _distance = Mathf.Clamp(value, 0.1f, 30f); }
    public override bool MakeADecision()
    {
        float calc = Vector2.Distance(_brain.Target.position, transform.position);

        if (calc < _distance)
        {
            if (boxCastUse)
            {
                RaycastHit2D playerCheck = DefineETC.BoxCast(RayPoint.position, new Vector2(Distance, 2), 0, new Vector2(_aiMovementData.direction.x, 0), 1, 1 << 6);
                if (playerCheck.collider != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else // 공격범위 안에 들어왔을때
                return true;
        }
        else
        {
            return false;
        }
    }
}
