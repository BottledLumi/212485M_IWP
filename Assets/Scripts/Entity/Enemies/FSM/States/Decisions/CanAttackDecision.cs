using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decision/CanAttack Decision")]
public class CanAttackDecision : Decision
{
    public override bool Decide(BaseStateMachine bsm)
    {
        if (bsm.enemy.canAttack)
            return true;
        return false;
    }
}
