using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Space]
    [Header("Default Property")]
    public float PhysicResistance;
    public float MagicResistance;
    public float MaxHealth;
    public float Vampirism;
    public float PhysicPower;
    public float MagicPower;
    [Space]
    [Header("Movement Property")]
    public float MoveSpeed;
    public float JumpHeight;
    public float MaxJumpCount;
    [Space]
    [Header("Dash Property")]
    public float DashSpeed;
    public float DashCoolTime;
    public float DashDistance;
    [Space]
    [Header("Teloportation Property")]
    public float TeleportationTime;
}
