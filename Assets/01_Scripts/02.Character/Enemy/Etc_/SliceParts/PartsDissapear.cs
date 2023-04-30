using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsDissapear : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ChangeSlice());
    }

    private IEnumerator ChangeSlice()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.layer = LayerMask.NameToLayer("Can'tCutting");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }
}
