using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Transition : ScriptableObject
{
    [SerializeField] BaseState targetState;
    [SerializeField] Decision decision;
    public void Execute(BaseStateMachine bsm)
    {
        if (decision.Decide(bsm))
            bsm.currentState = targetState;
    }
}
