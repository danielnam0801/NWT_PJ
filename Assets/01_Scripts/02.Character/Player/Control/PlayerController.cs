using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerState state;
    private PlayerInput input;

    private Dictionary<PlayerStateType, PlayerState> stateDictionary = new Dictionary<PlayerStateType, PlayerState>();

    
    [field: SerializeField]
    public bool Interactable { get; set; }
    [field: SerializeField]
    public bool IsBattle { get; set; }
    [field: SerializeField]
    public bool IsDie { get; set; }

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

        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        ChangeState(PlayerStateType.Movement);
        Interactable = true;
    }

    void Update()
    {
        if (IsDie)
            return;

        state.UpdateState();
    }

    public void ChangeState(PlayerStateType newState)
    {
        if(state != null) state.ExitState();
        state = stateDictionary[newState];
        state.EnterState();
    }

    public void Interact()
    {
        if(!Interactable) return;

        Debug.Log("interact");

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach(Collider2D col in cols)
        {
            if(col.TryGetComponent<IInteract>(out IInteract interact))
            {
                interact.Interact(gameObject);
                return;
            }
        }
    }

    public void Die()
    {
        ChangeState(PlayerStateType.Die);
        IsDie = true;
    }
}
