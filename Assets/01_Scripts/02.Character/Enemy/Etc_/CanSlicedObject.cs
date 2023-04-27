using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySpriteCutter;

public class CanSlicedObject : MonoBehaviour, ICuttable
{
    [SerializeField]
    int _headX, _headY, _headWidth, _headHeight, _textureWidth, _textureHeight;
    public void SetValues(int headX, int headY, int headWidth, int headHeight, int textureWidth, int textureHegiht)
    {
        this._headX = headX;
        this._headY = headY;
        this._headWidth = headWidth;
        this._headHeight = headHeight;
        this._textureWidth = textureWidth;
        this._textureHeight = textureHegiht;
    }
    
    public void SpriteCutting(Vector2 InputVec, Vector2 OutputVec, int layerMask = -1)
    {
        SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
        {
            lineStart = InputVec,
            lineEnd = OutputVec,
            headX = _headX,
            headY = _headY,
            headWidth = _headWidth,
            headHeight = _headHeight,    
            textureWidth = _textureWidth,
            textureHeight = _textureHeight,
            gameObject = this.gameObject,
            gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
        });

        if (output != null && output.secondSideGameObject != null)
        {
            //3초 후 사라지게
            output.firstSideGameObject.AddComponent<PartsDissapear>();
            output.secondSideGameObject.AddComponent<PartsDissapear>();
            output.secondSideGameObject.AddComponent<CanSlicedObject>();

            Rigidbody2D newRigidbody = output.secondSideGameObject.AddComponent<Rigidbody2D>();
            Rigidbody2D newRigidbody2;
            if (output.firstSideGameObject.GetComponent<Rigidbody2D>() == null)
                newRigidbody2 = output.firstSideGameObject.AddComponent<Rigidbody2D>();
            else
                newRigidbody2 = output.secondSideGameObject.GetComponent<Rigidbody2D>();

            newRigidbody.AddForceAtPosition((newRigidbody.position - InputVec) * 5, newRigidbody.position, ForceMode2D.Impulse);
            newRigidbody2.AddForceAtPosition((newRigidbody2.position - InputVec) * 5, newRigidbody2.position, ForceMode2D.Impulse);
        }
    }
}
