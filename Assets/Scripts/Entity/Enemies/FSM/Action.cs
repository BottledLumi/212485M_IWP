using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FSMAction : ScriptableObject
{
    abstract public void Execute(BaseStateMachine bsm);
}
