using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutorialText : MonoBehaviour
{
    public GuideText text;
    public List<string> texts = new List<string>();

    private int textIndex;

    private void Start()
    {
        //��Ŭ�� ����
        textIndex = -1;
    }

    private void Update()
    {
        if (!text.isActive)
            return;

        if(textIndex == -1)
        {
            textIndex++;
            text.SetText(texts[textIndex]);
        }
        else if(textIndex == 0)
        {
            if(Input.GetMouseButtonDown(1))
            {
                //��Ŭ�� ����
                textIndex++;
                text.SetText(texts[textIndex]);
            }
        }
        else if(textIndex == 1)
        {
            //q����
            if(Input.GetMouseButtonUp(0))
            {
                textIndex++;
                text.SetText(texts[textIndex]);
            }
        }
        else if(textIndex == 2)
        {
            //��ų ����
            if (Input.GetKeyDown(KeyCode.Q))
            {
                textIndex++;
                text.SetText(texts[textIndex]);
            }
        }
        else if (textIndex == 3)
        {
            //��ų ����
            if (Input.GetMouseButtonUp(0))
            {
                textIndex++;
                text.SetText(texts[textIndex]);
            }
        }
    }
}
