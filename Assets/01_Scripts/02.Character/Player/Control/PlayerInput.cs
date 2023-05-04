using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> OnMovementInput;
    public event Action OnSpaceInput;
    public event Action OnShiftInput;
    public event Action OnLeftClickInput;

    private Vector2 moveDir = Vector2.zero;

    private void Update()
    {
        UpdateMovementInput();
        UpdateSpaceInput();
        UpdateShiftInput();
        UpdateLeftClickInput();
    }
    //키보드
    public void UpdateMovementInput()
    {
        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        OnMovementInput?.Invoke(moveDir);
    }

    public void UpdateSpaceInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            OnSpaceInput?.Invoke();
    }

    public void UpdateShiftInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            OnShiftInput?.Invoke();
    }

    public void UpdateLeftClickInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnLeftClickInput?.Invoke();
    }

    //터치
    public void MoveInput(Vector2 input)
    {
        moveDir = input;
        //OnMovementInput?.Invoke(input);
    }

    public void DashInput()
    {
        OnShiftInput?.Invoke();
    }

    public void JumpInput()
    {
        OnSpaceInput?.Invoke();
    }

    public void TeleportationInput()
    {
        OnLeftClickInput?.Invoke();
    }
}
