using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Action/LockOnPlayer Action")]
public class LockOnPlayerAction : FSMAction
{
    [SerializeField] float lockOnDistance;
    [SerializeField] float unlockOnDistance;
    public override void Execute(BaseStateMachine bsm)
    {
        if (!bsm.enemy)
            return;
        if (!bsm.enemy.target && (lockOnDistance == 0 || Vector3.Distance(bsm.player.transform.position, bsm.transform.position) <= lockOnDistance))
            bsm.enemy.target = bsm.player;
        if (bsm.enemy.target && (unlockOnDistance != 0 && Vector3.Distance(bsm.player.transform.position, bsm.transform.position) >= unlockOnDistance))
            bsm.enemy.target = null;
    }
}
