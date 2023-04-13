using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> OnMovementInput;

    private void Update()
    {
        UpdateMovementInput();
    }

    private void UpdateMovementInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        OnMovementInput?.Invoke(input);
    }
}
