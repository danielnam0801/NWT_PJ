using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnitySpriteCutter;

public class EnemyParts : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite sprite;
    public int headX, headY, headWidth, headHeight, textureWidth, textureHeight;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CreateSameObject()
    {
        GameObject tmpObject;
        tmpObject = Instantiate(this.gameObject, transform.position, Quaternion.Euler(transform.eulerAngles));
        tmpObject.transform.parent = this.transform; // 지금 내 위치를 부모로 지정
        
        Destroy(tmpObject.GetComponent<SpriteSkin>());
        
        SpriteRenderer childSpriteRender = tmpObject.GetComponent<SpriteRenderer>();
        childSpriteRender.sprite = sprite;
        childSpriteRender.sortingOrder = 5;
        tmpObject.layer = LayerMask.NameToLayer("CanCutted");
        CanSlicedObject obj = tmpObject.AddComponent<CanSlicedObject>();
        obj.SetValues(headX, headY, headWidth, headHeight, textureWidth, textureHeight);

        SetSpriteRenderEnabled(false);
    }

    public void SetSpriteRenderEnabled(bool value)
    {
        spriteRenderer.enabled = value;
    }
}
