using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "FSM/MoveToTarget Action")]
public class MoveToTargetAction : Action
{
    [SerializeField] float distance;

    AIDestinationSetter destinationSetter;
    AIPath aiPath;
    public override void Execute(BaseStateMachine bsm)
    {
        if (!bsm.enemy.target)
        {
            if (destinationSetter && destinationSetter.target) destinationSetter.target = null;
            if (aiPath) aiPath.enabled = false;
            return;
        }
        
        if (!destinationSetter || !aiPath)
        {
            destinationSetter = bsm.GetComponent<AIDestinationSetter>();
            aiPath = bsm.GetComponent<AIPath>();
        }

        if (Vector3.Distance(bsm.enemy.transform.position, bsm.enemy.target.transform.position) > distance)
        {
            aiPath.enabled = true;
            destinationSetter.target = bsm.enemy.target.transform;
        }
        else
            destinationSetter.target = null;

        if (bsm.enemy.DamageTaken())
            aiPath.enabled = false;
    }
}
