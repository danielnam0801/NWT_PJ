using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerWeapon weapon;
    public PlayerWeapon Weapon { get => weapon; set => weapon = value; }

    private void Start()
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }
}
