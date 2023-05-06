using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnitySpriteCutter;

public class EnemySpriteParts : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Vector2 scale;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void CreateSameObject()
    {
        GameObject tmpObject;
        tmpObject = Instantiate(this.gameObject, transform.position, Quaternion.Euler(transform.eulerAngles));
        Destroy(tmpObject.GetComponent<SpriteSkin>());

        SpriteRenderer childSpriteRender = tmpObject.GetComponent<SpriteRenderer>();
        childSpriteRender.sprite = spriteRenderer.sprite;
        childSpriteRender.sortingOrder = spriteRenderer.sortingOrder;
        tmpObject.AddComponent<BoxCollider2D>();
        tmpObject.AddComponent<Rigidbody2D>();

        CanSlicedObject obj = tmpObject.AddComponent<CanSlicedObject>();
        tmpObject.layer = LayerMask.NameToLayer("CanCutted");

        SetSpriteRenderEnabled(false);
    }

    public void SetSpriteRenderEnabled(bool value)
    {
        spriteRenderer.enabled = value;
    }
}
