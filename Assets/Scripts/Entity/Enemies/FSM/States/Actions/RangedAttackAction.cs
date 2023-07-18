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
        if (!bsm.enemy.CanAttack)
            return;

        GameObject projectile = GameObject.Instantiate(bulletPrefab);
        projectile.GetComponent<EnemyProjectile>().SetOwner(bsm.enemy);

        // Face target
        {
            Vector3 direction = bsm.enemy.target.transform.position - bsm.transform.position;
            direction.Normalize();
            bsm.transform.up = direction;
        }

        bsm.enemy.CanAttack = false;
    }
}
