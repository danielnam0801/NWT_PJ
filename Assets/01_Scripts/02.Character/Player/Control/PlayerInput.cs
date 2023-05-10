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
    public event Action OnMouseUpAction;

    private Vector2 moveDir = Vector2.zero;

    private void Update()
    {
        UpdateMovementInput();
        UpdateJumpInput();
        UpdateDashInput();
        UpdateLeftClickInput();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            OnMouseUpAction?.Invoke();
    }
    //Ű����
    public void UpdateMovementInput()
    {
        Debug.Log(moveDir);
        OnMovementInput?.Invoke(moveDir);
    }

    public void UpdateJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnSpaceInput?.Invoke();
    }

    public void UpdateDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            OnShiftInput?.Invoke();
    }

    public void UpdateLeftClickInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnLeftClickInput?.Invoke();
    }

    //��ġ
    public void MoveInput(Vector2 input)
    {
        moveDir = input;
    }

    public void StopMoveInput()
    {
        moveDir = Vector2.zero;
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