using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Action/LockOnPlayer Action")]
public class LockOnPlayerAction : FSMAction
{
    GameObject player;

    [SerializeField] float lockOnDistance;
    [SerializeField] float unlockOnDistance;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Execute(BaseStateMachine bsm)
    {
        if (!bsm.enemy)
            return;
        if (!bsm.enemy.target && (lockOnDistance == 0 || Vector3.Distance(player.transform.position, bsm.transform.position) <= lockOnDistance))
            bsm.enemy.target = player;
        if (bsm.enemy.target && (unlockOnDistance != 0 && Vector3.Distance(player.transform.position, bsm.transform.position) >= unlockOnDistance))
            bsm.enemy.target = null;
    }
}
