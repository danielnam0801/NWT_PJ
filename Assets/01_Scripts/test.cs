using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0000000000001f;
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        while (true)
        {

            Debug.Log(1);

            yield return null;
        }
    }
}//6 900
