using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/LockOnPlayer Action")]
public class LockOnPlayerAction : Action
{
    GameObject player = GameObject.FindGameObjectWithTag("Player");

    [SerializeField] float lockOnDistance;
    [SerializeField] float unlockOnDistance;

    public override void Execute(BaseStateMachine bsm)
    {
        if (!bsm.enemy)
            return;
        if (!bsm.enemy.target && (lockOnDistance == 0 || Vector3.Distance(player.transform.position, bsm.transform.position) <= lockOnDistance))
            bsm.enemy.target = player;
        if (bsm.enemy.target && (unlockOnDistance != 0 || Vector3.Distance(player.transform.position, bsm.transform.position) >= unlockOnDistance))
            bsm.enemy.target = null;
    }
}
