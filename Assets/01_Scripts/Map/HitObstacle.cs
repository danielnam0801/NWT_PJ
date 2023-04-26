using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitObstacle : MonoBehaviour
{
    public UnityEvent ExecutionEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
            ExecutionEvent?.Invoke();
        }
    }
}
