using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Transition : ScriptableObject
{
    [SerializeField] BaseState targetState;
    [SerializeField] Decision decision;
    public void Execute(BaseStateMachine baseStateMachine)
    {
        if (decision.Decide())
            baseStateMachine.currentState = targetState;
    }
}
