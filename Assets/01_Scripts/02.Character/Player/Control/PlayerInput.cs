using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ExtraInput
{
    public KeyCode keyCode;
    public UnityEvent inputAction;
    public UnityEvent outputAction;
}

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> OnMovementInput;
    public event Action OnSpaceInput;
    public event Action OnShiftInput;
    public event Action OnLeftClickInput;
    public event Action OnMouseUpAction;

    //public ShapeType Q_Type;
    //public UnityEvent<ShapeType> Q_Input;
    //public ShapeType E_Type;
    //public UnityEvent<ShapeType> E_Input;
    //public ShapeType R_Type;
    //public UnityEvent<ShapeType> R_Input;
    //public ShapeType T_Type;
    //public UnityEvent<ShapeType> T_Input;

    [SerializeField]
    private List<ExtraInput> extraInputList;
    private Dictionary<KeyCode, ExtraInput> extraInputDictionary = new Dictionary<KeyCode, ExtraInput>();
 
    private Vector2 moveDir = Vector2.zero;

    private void Awake()
    {
        foreach(ExtraInput extraInput in extraInputList)
        {
            extraInputDictionary.Add(extraInput.keyCode, extraInput);
        }
    }

    private void Update()
    {
        UpdateMovementInput();
        UpdateJumpInput();
        UpdateDashInput();
        UpdateLeftClickInput();
        //SkillInput();
        UpdateExtraInput();

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
        OnMovementInput?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), 0));
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

    //private void SkillInput()
    //{
    //    if(Input.anyKeyDown)
    //    {
    //        if(Input.GetKeyDown(KeyCode.Q))
    //        {
    //            Q_Input?.Invoke(Q_Type);
    //        }
    //        else if(Input.GetKeyDown(KeyCode.E))
    //        {
    //            Q_Input?.Invoke(E_Type);
    //        }
    //        else if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            Q_Input?.Invoke(R_Type);
    //        }
    //        else if (Input.GetKeyDown(KeyCode.T))
    //        {
    //            Q_Input?.Invoke(T_Type);
    //        }
    //    }
    //}

    public void AddInput(KeyCode code, UnityAction _inputAction, UnityAction _outputAction = null)
    {
        if(extraInputDictionary.ContainsKey(code))
        {
            extraInputDictionary[code].inputAction.AddListener(_inputAction);
            extraInputDictionary[code].outputAction.AddListener(_outputAction);
        }
        else
        {
            Debug.Log("등록된 키 없음");
        }
    }

    private void UpdateExtraInput()
    {
        foreach(ExtraInput input in extraInputList)
        {
            if (Input.GetKeyDown(input.keyCode))
                input.inputAction?.Invoke();
            else if(Input.GetKeyUp(input.keyCode))
                input.outputAction?.Invoke();
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