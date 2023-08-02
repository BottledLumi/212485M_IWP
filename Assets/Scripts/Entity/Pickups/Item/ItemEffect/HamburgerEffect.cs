using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HamburgerEffect", menuName = "ItemEffect/HamburgerEffect")]
public class HamburgerEffect : ItemEffect
{
    PlayerData playerData;
    PlayerWeapon playerWeapon;

    float baseMultiplier = 1.5f;

    uint attackCounter = 0;

    public override void OnAdd()
    {
        playerData = PlayerData.Instance;

        playerWeapon = ItemsManager.Instance.player.GetComponent<PlayerWeapon>();
        playerWeapon.AttackEvent += OnAttack;
    }

    private void OnAttack(List<Enemy> enemies)
    {
        if (enemies.Count == 0)
            return;

        attackCounter++;
        if (attackCounter >= 4)
        {
            attackCounter = 0;
            foreach (Enemy enemy in enemies)
            {
                float baseDamage = playerData.Attack * playerData.Weapon.getAttack();
                enemy.TakeDamage(baseDamage * DamageIncreaseMultiplier());
            }
        }
    }

    private float DamageIncreaseMultiplier()
    {
        return Mathf.Pow(baseMultiplier, Value)-1;
    }
}
