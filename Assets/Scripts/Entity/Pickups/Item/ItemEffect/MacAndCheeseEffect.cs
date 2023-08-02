using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MacAndCheeseEffect", menuName = "ItemEffect/MacAndCheeseEffect")]
public class MacAndCheeseEffect : ItemEffect
{
    PlayerData playerData;
    PlayerWeapon playerWeapon;

    public override void OnAdd()
    {
        playerData = PlayerData.Instance;

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
        float extraPercentage = 1 - playerData.Health / playerData.MaxHealth;
        return Value * extraPercentage * playerData.Attack * playerData.Weapon.getAttack();
    }

    void OnEnemyHit(Enemy enemy)
    {
        enemy.TakeDamage(ExtraDamage());
    }
}
