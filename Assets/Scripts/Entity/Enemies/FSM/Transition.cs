using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition")]
abstract public class Transition : ScriptableObject
{
    [SerializeField] BaseState trueState;
    [SerializeField] BaseState falseState;

    [SerializeField] Decision decision;
    public void Execute(BaseStateMachine bsm)
    {
        if (decision.Decide(bsm))
            bsm.currentState = trueState;
        else if (!(falseState is RemainInState))
            bsm.currentState = falseState;
    }
}
