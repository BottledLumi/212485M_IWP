using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;

[CreateAssetMenu(menuName = "FSM/Action/MoveToTarget Action")]
public class MoveToTargetAction : FSMAction
{
    [SerializeField] float distance;
    [SerializeField] bool disableRotation;
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

        if (!aiPath.enabled) // Face target
        {
            Vector3 direction = bsm.enemy.target.transform.position - bsm.transform.position;
            direction.Normalize();
            bsm.transform.up = direction;
        }

        if (disableRotation)
            bsm.transform.rotation = Quaternion.identity;

        if (Vector3.Distance(bsm.enemy.transform.position, bsm.enemy.target.transform.position) > distance)
        {
            aiPath.enabled = true;
            destinationSetter.target = bsm.enemy.target.transform;
        }
        else
        {
            aiPath.enabled = false;
            destinationSetter.target = null;
        }
    }
}
