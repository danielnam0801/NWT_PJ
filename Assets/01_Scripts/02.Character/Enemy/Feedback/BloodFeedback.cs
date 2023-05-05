using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFeedback : Feedback
{
    public override void CreateFeedBack()
    {
        Debug.Log("player blood effect");
    }

    public override void FinishFeedBack()
    {
        
    }
}
