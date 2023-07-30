using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New PotatoEffect", menuName = "ItemEffect/PotatoEffect")]
public class PotatoEffect : ItemEffect
{
    float baseMultiplier = 1.1f;

    PlayerData playerData;

    PlayerWeapon playerWeapon;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;
        playerWeapon = ItemsManager.Instance.player.GetComponent<PlayerWeapon>();
        playerWeapon.EnemyHitEvent += OnEnemyHit;
    }

    public override void OnRemove()
    {
        playerWeapon.EnemyHitEvent -= OnEnemyHit;
    }

    void OnEnemyHit(Enemy enemy)
    {
        if (enemy is Boss)
            enemy.TakeDamage(ExtraDamage());
    }

    private float ExtraDamage()
    {
        return (Mathf.Pow(baseMultiplier, Value)-1) * playerData.Attack * playerData.Weapon.getAttack();
    }
}
