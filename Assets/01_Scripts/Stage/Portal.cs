using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInteract
{
    public void Interact()
    {
        //�������� ����
        //StageManager.Instance.ChangeStage();
        Debug.Log("Change Stage");  
    }
}
