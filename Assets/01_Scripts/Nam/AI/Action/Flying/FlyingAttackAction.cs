using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackAction : AIAction
{
    public override void TakeAction()
    {
        Debug.Log("AttackAction");
        _aiActionData.isIdle = false;
        //_aiMovementData.direction = Vector2.zero;
        if (_aiActionData.isAttack == false)
        {
            _brain.Attack();
            _aiMovementData.pointOfInterest = _brain.Target.position;
        }
        //_brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest); // flying movingø° ¿÷¿Ω
    }

}
