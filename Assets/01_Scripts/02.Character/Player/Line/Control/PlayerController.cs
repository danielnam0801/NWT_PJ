using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerState state;
    private Dictionary<PlayerStateType, PlayerState> stateDictionary = new Dictionary<PlayerStateType, PlayerState>();

    private void Awake()
    {
        Transform stateTrm = transform.Find("States");

        foreach(PlayerStateType state in Enum.GetValues(typeof(PlayerStateType)))
        {
            PlayerState stateScript = stateTrm.GetComponent($"{state}State") as PlayerState;

            if (stateScript == null)
            {
                Debug.Log($"{state}스크립트 없음");
            }

            stateDictionary.Add(state, stateScript);
        }
    }

    private void Start()
    {
        ChangeState(PlayerStateType.Idle);
    }

    void Update()
    {
        //Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0);
        //GetComponent<PlayerMovement>().Move(moveInput);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    GetComponent<PlayerMovement>().Jump();
        //}
        state.UpdateState();
    }

    public void ChangeState(PlayerStateType newState)
    {
        state.ExitState();
        state = stateDictionary[newState];
        state.EnterState();
    }
}
