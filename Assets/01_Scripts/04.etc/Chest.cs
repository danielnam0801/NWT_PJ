using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IInteract
{
    private Animator animator;

    private int openHash = Animator.StringToHash("IsOpened");

    [field: SerializeField]
    public bool IsOpen = false;

    public ShapeType HoldShape;

    public UnityEvent<ShapeType> OpenAction;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void Start()
    {
        OpenAction.AddListener(GameManager.instance.Target.GetComponent<PlayerInventory>().AddShape);
    }

    public void Interact(GameObject Sender)
    {
        if (IsOpen)
            return;

        OpenAction?.Invoke(HoldShape);
    }   

    public void ChangeState(bool isOpen)
    {
        IsOpen = isOpen;
        animator.SetBool(openHash, isOpen);
    }
}
