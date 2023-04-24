using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnitySpriteCutter;

public class EnemyParts : MonoBehaviour,ICuttable
{
    public void SpriteCutting(Vector2 InputVec, Vector2 OutputVec, int layerMask = -1)
    {
        //Destroy(GetComponent<SpriteSkin>());
        SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
        {
            lineStart = InputVec,
            lineEnd = OutputVec,
            gameObject = this.gameObject,
            gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
        });

        if (output != null && output.secondSideGameObject != null)
        {
            //Rigidbody2D newRigidbody = output.secondSideGameObject.AddComponent<Rigidbody2D>();
            //newRigidbody.velocity = output.firstSideGameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }
}
