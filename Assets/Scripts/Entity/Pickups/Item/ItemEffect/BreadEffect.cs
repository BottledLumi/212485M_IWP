using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BreadEffect", menuName = "ItemEffect/BreadEffect")]
public class BreadEffect : ItemEffect
{
    PlayerWeapon playerWeapon;

    float baseExtraDamage = 3f;

    public override void OnAdd()
    {
        playerWeapon = ItemsManager.Instance.player.GetComponent<PlayerWeapon>();
        playerWeapon.EnemyHitEvent += OnEnemyHit;
    }
    public override void Execute()
    {
    }
    public override void OnRemove()
    {
        playerWeapon.EnemyHitEvent -= OnEnemyHit;
    }

    private float ExtraDamage()
    {
        return Value * baseExtraDamage;
    }

    void OnEnemyHit(Enemy enemy)
    {
        float extraDamage = ExtraDamage() * enemy.Level;
        enemy.TakeDamage(extraDamage);
        Debug.Log(extraDamage);
    }
}
