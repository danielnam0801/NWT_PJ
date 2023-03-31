using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
