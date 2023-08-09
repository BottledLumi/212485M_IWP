using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decision/Health Decision")]
public class HealthDecision : Decision
{
    [SerializeField] float healthThreshold;
    public override bool Decide(BaseStateMachine bsm)
    {
        if (bsm.enemy.Health < healthThreshold)
            return true;
        return false;
    }
}
