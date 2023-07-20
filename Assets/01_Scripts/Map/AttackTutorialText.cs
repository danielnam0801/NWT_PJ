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
        //우클릭 설명
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
                //좌클릭 설명
                textIndex++;
                text.SetText(texts[textIndex]);
            }
        }
        else if(textIndex == 1)
        {
            //q설명
            if(Input.GetMouseButtonUp(0))
            {
                textIndex++;
                text.SetText(texts[textIndex]);
            }
        }
        else if(textIndex == 2)
        {
            //스킬 설명
            if (Input.GetKeyDown(KeyCode.Q))
            {
                textIndex++;
                text.SetText(texts[textIndex]);
            }
        }
        else if (textIndex == 3)
        {
            //스킬 설명
            if (Input.GetMouseButtonUp(0))
            {
                textIndex++;
                text.SetText(texts[textIndex]);
            }
        }
    }
}
