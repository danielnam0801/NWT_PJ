using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillName
{
    FlyingNormalAttack, FrogJumpAttack, FrogTongueAttack, NormalMeleeAttack
}
public class AIStateInfo : MonoBehaviour
{
    public bool IsAttack = false;
    public bool IsFrogTongueAttack = false;
    public bool IsFrogJumpAttack = false;
}
