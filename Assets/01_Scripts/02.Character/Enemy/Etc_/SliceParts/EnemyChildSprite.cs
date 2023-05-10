using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnitySpriteCutter;

public class EnemyChildSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Vector2 scale;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void CreateSameObject()
    {
        GameObject tmpObject = Instantiate(this.gameObject, transform.position, Quaternion.identity);
        tmpObject.transform.SetParent(this.transform);
        tmpObject.AddComponent<BoxCollider2D>();
        tmpObject.AddComponent<Rigidbody2D>();

        CanSlicedObject obj = tmpObject.AddComponent<CanSlicedObject>();
        obj.SetValues(true);
        tmpObject.layer = LayerMask.NameToLayer("CanCutted");

        SetSpriteRenderEnabled(false);
    }

    public void SetSpriteRenderEnabled(bool value)
    {
        spriteRenderer.enabled = value;
    }
}
