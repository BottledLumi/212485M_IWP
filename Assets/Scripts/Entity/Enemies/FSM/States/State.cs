using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "FSM/State")]
public sealed class State : BaseState
{
    [SerializeField] List<Action> actions = new List<Action>();
    [SerializeField] List<Transition> transitions = new List<Transition>();
    public override void Execute(BaseStateMachine bsm)
    {
        foreach (Action action in actions)
            action.Execute(bsm);
        foreach (var transition in transitions)
            transition.Execute(bsm);
    }
}
