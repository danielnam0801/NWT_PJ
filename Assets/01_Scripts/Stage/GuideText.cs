using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideText : MonoBehaviour
{
    public TextMeshPro text;
    public float activeDistance;
    public string guideText;

    private Transform playerTrm;
    public bool isActive = false;

    private void Start()
    {
        playerTrm = GameManager.instance.Target;
        text.text = guideText;  
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, playerTrm.position) <= activeDistance)
        {
            if(!isActive)
            {
                isActive = true;
            }
        }
        else
        {
            if(isActive)
            {
                isActive = false;
            }
        }
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }
}
