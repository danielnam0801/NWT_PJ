using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> OnMovementInput;
    public event Action OnSpaceInput;
    public event Action OnShiftInput;
    public event Action OnLeftClickInput;
    public event Action OnMouseUpAction;

    public ShapeType Q_Type;
    public UnityEvent<ShapeType> Q_Input;
    public ShapeType E_Type;
    public UnityEvent<ShapeType> E_Input;
    public ShapeType R_Type;
    public UnityEvent<ShapeType> R_Input;
    public ShapeType T_Type;
    public UnityEvent<ShapeType> T_Input;
 
    private Vector2 moveDir = Vector2.zero;

    private void Update()
    {
        UpdateMovementInput();
        UpdateJumpInput();
        UpdateDashInput();
        UpdateLeftClickInput();
        SkillInput();

        //모바일
        //if (Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //    MoveInput(Vector2.zero);
        //}
    }

    #region 키보드
    //키보드
    public void UpdateMovementInput()
    {
        OnMovementInput?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
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

    private void SkillInput()
    {
        if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Q_Input?.Invoke(Q_Type);
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                Q_Input?.Invoke(E_Type);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Q_Input?.Invoke(R_Type);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                Q_Input?.Invoke(T_Type);
            }
        }
    }
    #endregion

    #region 터치
    //public void UpdateMovementInput()
    //{
    //    //Debug.Log(moveDir);

    //    //OnMovementInput?.Invoke(moveDir);
    //}

    //public void MoveInput(Vector2 input)
    //{
    //    moveDir = input;

    //    OnMovementInput?.Invoke(moveDir);
    //}

    //public void DashInput()
    //{
    //    OnShiftInput?.Invoke();
    //}

    //public void JumpInput()
    //{
    //    OnSpaceInput?.Invoke();
    //}

    //public void TeleportationInput()
    //{
    //    OnLeftClickInput?.Invoke();
    //}
    #endregion
}