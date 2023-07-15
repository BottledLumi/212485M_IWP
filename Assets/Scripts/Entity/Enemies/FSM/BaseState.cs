using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseState : ScriptableObject
{
    [SerializeField] List<Action> actions = new List<Action>();
    [SerializeField] List<Transition> transitions = new List<Transition>();
    public void Execute(BaseStateMachine baseStateMachine)
    {
        foreach (Action action in actions)
            action.Execute();
        foreach (var transition in transitions)
            transition.Execute(baseStateMachine);
    }
}
