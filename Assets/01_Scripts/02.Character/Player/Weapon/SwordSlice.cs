using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlice : MonoBehaviour
{
    public float damage = 10;
    Vector2 InputPos;
    Vector2 OutPutPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("CanCutted") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
           InputPos = collision.ClosestPoint(transform.position);   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ICuttable cuttable = null;
        if (collision.gameObject.layer == LayerMask.NameToLayer("CanCutted"))
        {
            OutPutPos = collision.ClosestPoint(transform.position);
            if (collision.gameObject.TryGetComponent<ICuttable>(out cuttable))
            {
                Debug.Log("iScUT");
                cuttable.SpriteCutting(InputPos, OutPutPos, LayerMask.NameToLayer("CanCutted"));
            }
        }
    }
}
