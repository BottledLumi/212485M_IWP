using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "FSM/Action/MoveToTarget Action")]
public class MoveToTargetAction : FSMAction
{
    [SerializeField] float distance;

    public override void Execute(BaseStateMachine bsm)
    {
        AIDestinationSetter destinationSetter = bsm.GetComponent<AIDestinationSetter>();
        AIPath aiPath = bsm.GetComponent<AIPath>();

        if (!bsm.enemy.target)
        {
            if (destinationSetter && destinationSetter.target) destinationSetter.target = null;
            if (aiPath) aiPath.enabled = false;
            return;
        }

        if (bsm.enemy.DamageTaken)
        {
            aiPath.enabled = false;
            return;
        }

        if (Vector3.Distance(bsm.enemy.transform.position, bsm.enemy.target.transform.position) > distance)
        {
            aiPath.enabled = true;
            destinationSetter.target = bsm.enemy.target.transform;
        }
        else
            destinationSetter.target = null;
    }
}
