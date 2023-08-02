using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MangoCakeEffect", menuName = "ItemEffect/MangoCakeEffect")]
public class MangoCakeEffect : ItemEffect
{
    float baseHeal = 3f;

    PlayerWeapon playerWeapon;

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

    void OnEnemyHit(Enemy enemy)
    {
        if (!enemy.gameObject.activeInHierarchy)
            playerWeapon.GetComponent<Player>().Heal(baseHeal * Value);
    }
}
