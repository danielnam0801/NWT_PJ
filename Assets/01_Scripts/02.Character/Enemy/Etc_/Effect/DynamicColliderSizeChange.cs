using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicColliderSizeChange : MonoBehaviour
{
    [SerializeField]
    [Range(0f,1f)] private float sizeProportion;
    CircleCollider2D circleCollider;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.offset = Vector2.zero;
        Debug.Log(transform.localScale.x);
        circleCollider.radius *= sizeProportion;
    }
}
