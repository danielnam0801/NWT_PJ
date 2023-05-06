using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnitySpriteCutter;

public class EnemyBoneParts : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] EnemyPartsUVData partsData;
    [SerializeField] Material mat;
    public int headX, headY, headWidth, headHeight, textureWidth, textureHeight; 
    Vector2 scale;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scale = transform.lossyScale;
    }

    private void Start()
    {
        SetValues();
    }

    private void SetValues()
    {
        headX = partsData.GetHeadX;
        headY = partsData.GetHeadY;
        headWidth = partsData.GetHeadWidth;
        headHeight = partsData.GetHeadHeight;
        textureWidth = partsData.GetTextureWidth;
        textureHeight = partsData.GetTextureHeight;
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
        obj.SetValues(headX, headY, headWidth, headHeight, textureWidth, textureHeight, scale, transform, null);
        
        tmpObject.layer = LayerMask.NameToLayer("CanCutted");

        SetSpriteRenderEnabled(false);
    }

    public void SetSpriteRenderEnabled(bool value)
    {
        spriteRenderer.enabled = value;
    }
}
