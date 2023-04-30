using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public float physicResistance;
    public float magicResistance;
    public float healthIncrement;
    public float vampirism;
    public float physicPowerIncreament;
    public float magicPowerIncreament;
    public float moveSpeedIncreament;
}
