using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnitySpriteCutter;

public class EnemyParts : MonoBehaviour, ICuttable
{
    Action destroyAct;
    
    public void SpriteCutting(Vector2 InputVec, Vector2 OutputVec, int layerMask = -1)
    {
        destroyAct += ( ) =>
        {
            Destroy(GetComponent<SpriteSkin>());
        };
        SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
        {
            lineStart = InputVec,
            lineEnd = OutputVec,
            gameObject = this.gameObject,
            destroySpriteSkinAct = destroyAct,
            gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
        });

        if (output != null && output.secondSideGameObject != null)
        {
            //3초 후 사라지게
            output.firstSideGameObject.AddComponent<PartsDissapear>();
            output.secondSideGameObject.AddComponent<PartsDissapear>();

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
