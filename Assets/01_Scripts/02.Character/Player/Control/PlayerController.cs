using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerState state;
    //public PlayerInput input;

    private Dictionary<PlayerStateType, PlayerState> stateDictionary = new Dictionary<PlayerStateType, PlayerState>();

    private void Awake()
    {
        Transform stateTrm = transform.Find("States");

        foreach(PlayerStateType state in Enum.GetValues(typeof(PlayerStateType)))
        {
            PlayerState stateScript = stateTrm.GetComponent($"Player{state}State") as PlayerState;

            if (stateScript == null)
            {
                Debug.Log($"{state}스크립트 없음");
                continue;
            }

            stateScript.Init(transform);
            stateDictionary.Add(state, stateScript);
        }
    }

    private void Start()
    {
        ChangeState(PlayerStateType.Movement);
    }

    void Update()
    {
        state.UpdateState();
    }

    public void ChangeState(PlayerStateType newState)
    {
        if(state != null) state.ExitState();
        state = stateDictionary[newState];
        state.EnterState();
    }
}
