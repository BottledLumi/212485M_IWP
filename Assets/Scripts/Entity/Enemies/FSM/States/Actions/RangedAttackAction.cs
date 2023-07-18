using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Action/RangedAttack Action")]
public class RangedAttackAction : FSMAction
{
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] 
    public override void Execute(BaseStateMachine bsm)
    {
        if (!bsm.enemy.canAttack)
            return;



        bsm.enemy.canAttack = false;
    }
}
