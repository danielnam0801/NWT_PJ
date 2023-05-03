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

    private void Update()
    {
        //UpdateMovementInput();
        UpdateSpaceInput();
        UpdateShiftInput();
        UpdateLeftClickInput();
    }

    public void UpdateMovementInput(Vector2 input)
    {
        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        OnMovementInput?.Invoke(input);
    }

    private void UpdateSpaceInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            OnSpaceInput?.Invoke();
    }

    private void UpdateShiftInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            OnShiftInput?.Invoke();
    }

    private void UpdateLeftClickInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnLeftClickInput?.Invoke();
    }
}
