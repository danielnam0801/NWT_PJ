using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    public float damage = 2f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")
            other.GetComponent<PlayerHealth>().GetHit(damage, gameObject, Vector3.zero);
    }
}
