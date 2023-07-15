using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] BaseState initialState;
    public BaseState currentState;

    private void Awake()
    {
        currentState = initialState;
    }

    private void Update()
    {
        currentState.Execute(this);
    }
}
