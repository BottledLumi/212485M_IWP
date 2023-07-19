using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Action/RangedAttack Action")]
public class RangedAttackAction : FSMAction
{
    [SerializeField] GameObject bulletPrefab;

    public override void Execute(BaseStateMachine bsm)
    {
        if (!bsm.enemy.CanAttack)
            return;

        GameObject projectile = GameObject.Instantiate(bulletPrefab);
        projectile.GetComponent<EnemyProjectile>().SetOwner(bsm.enemy);
        
        bsm.enemy.CanAttack = false;
    }
}
