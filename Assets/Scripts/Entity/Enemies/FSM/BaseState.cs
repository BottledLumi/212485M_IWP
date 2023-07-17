using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseState : ScriptableObject
{
    public virtual void Execute(BaseStateMachine bsm) { }
}
