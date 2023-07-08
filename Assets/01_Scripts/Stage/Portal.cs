using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInteract
{
    public void Interact()
    {
        //스테이지 변경
        //StageManager.Instance.ChangeStage();
        Debug.Log("Change Stage");  
    }
}
