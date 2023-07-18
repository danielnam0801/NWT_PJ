using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInteract
{
    public void Interact(GameObject Sender)
    {
        StageManager.Instance.ChangeStage();
    }
}
