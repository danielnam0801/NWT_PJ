using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    private PlayerWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<PlayerWeapon>();
    }

    
}
