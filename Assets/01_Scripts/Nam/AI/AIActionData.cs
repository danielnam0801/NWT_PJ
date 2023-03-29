using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionData : MonoBehaviour
{
    public bool isCanThinking = true;
    public bool isAttack;//현재 공격중인가
    public bool targetSpotted;//타겟을 발견했는가
    public bool arrived;

    public bool isIdle;
    public bool isShield = false;

}
