using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LightManager.Instance.AddFocusObject(gameObject);   
    }
}
