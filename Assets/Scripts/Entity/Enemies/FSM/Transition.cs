using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Transition : ScriptableObject
{
    [SerializeField] BaseState targetState;
    public void Execute(BaseStateMachine baseStateMachine)
    {
        if (Condition())
            baseStateMachine.currentState = targetState;
    }
    abstract public bool Condition();
}
