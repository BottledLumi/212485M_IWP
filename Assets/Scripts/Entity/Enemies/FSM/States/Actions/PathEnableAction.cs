using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

[CreateAssetMenu(menuName = "FSM/Action/PathEnable Action")]
public class PathEnableAction : FSMAction
{
    public override void Execute(BaseStateMachine bsm)
    {
        bsm.GetComponent<AIPath>().enabled = true;
    }
}
