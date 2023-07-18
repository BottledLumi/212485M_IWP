using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "FSM/State")]
public sealed class State : BaseState
{
    [SerializeField] List<FSMAction> actions = new List<FSMAction>();
    [SerializeField] List<Transition> transitions = new List<Transition>();
    public override void Execute(BaseStateMachine bsm)
    {
        foreach (FSMAction action in actions)
            action.Execute(bsm);
        foreach (var transition in transitions)
            transition.Execute(bsm);
    }
}
