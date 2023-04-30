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

    [Space]
    [Header("Equipment")]
    public List<Item> items = new List<Item>();

    public void MountingItem(Item item)
    {
        items.Add(item);
        PhysicResistance += item.info.physicResistance;
        MagicResistance += item.info.magicResistance;
        MaxHealth += item.info.healthIncrement;
        Vampirism += item.info.vampirism;
        PhysicPower += item.info.physicPowerIncreament;
        MagicPower += item.info.magicPowerIncreament;
    }

    public void ReleaseItem(Item item)
    {
        items.Remove(item);
        PhysicResistance -= item.info.physicResistance;
        MagicResistance -= item.info.magicResistance;
        MaxHealth -= item.info.healthIncrement;
        Vampirism -= item.info.vampirism;
        PhysicPower -= item.info.physicPowerIncreament;
        MagicPower -= item.info.magicPowerIncreament;
    }
}
