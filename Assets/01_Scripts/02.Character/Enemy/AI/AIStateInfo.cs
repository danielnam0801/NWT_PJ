using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillName
{
    Normal,
    Special,
    Jump,
    Melee,
    Range
}
public class AIStateInfo : MonoBehaviour
{
    //[Header("cooltime")]
    //public float NormalCool = 0f;
    //public float SpecialCool = 0f;
    //public float JumpCool = 0f;
    //public float MeleeCool = 0f;
    //public float RangeCool = 0f;

    [Header("bool")]
    public bool IsAttack = false;
    public bool IsNormal = false;
    public bool IsSpecial = false;
    public bool IsJump = false;
    public bool IsMelee = false;
    public bool IsRange = false;
}
