using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "FSM/Action/AwayFromTarget Action")]
public class AwayFromTargetAction : FSMAction
{
    [SerializeField] float distance;
    [SerializeField] float speed;

    public override void Execute(BaseStateMachine bsm)
    {
        Vector2 direction = bsm.transform.position - bsm.enemy.target.transform.position;
        direction.Normalize();
        bsm.transform.position = Vector2.MoveTowards(bsm.transform.position, bsm.transform.position + new Vector3(direction.x, direction.y, 0), bsm.enemy.MovementSpeed * speed * Time.deltaTime);
        
        bsm.GetComponent<AIDestinationSetter>().target = bsm.transform;
    }
}
