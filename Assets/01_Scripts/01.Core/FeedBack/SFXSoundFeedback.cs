using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSoundFeedback : Feedback
{
    public string AudioName;

    public override void CreateFeedBack()
    {
        AudioManager.Instance.PlaySFX(AudioName);
    }

    public override void FinishFeedBack()
    {
        
    }
}
