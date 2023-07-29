using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SteakEffect", menuName = "ItemEffect/SteakEffect")]
public class SteakEffect : ItemEffect
{
    int basePiercingValue = 1;
    int totalPiercingValue;

    float baseExtraMeleeDamage = 4f;

    Weapon weapon;
    RangedWeapon rangedWeapon;
    public override void OnAdd()
    {
        PlayerData playerData = PlayerData.Instance;

        ValueChangedEvent += OnValueChanged; playerData.WeaponChangedEvent += OnWeaponChanged;

        weapon = playerData.Weapon;
        if (weapon)
        {
            rangedWeapon = weapon as RangedWeapon;

            totalPiercingValue = basePiercingValue;

            SwapStats(true);
        }
        
    }
    private void OnValueChanged(int value)
    {
        if (rangedWeapon)
        {
            int piercingToAdd = totalPiercingValue;
            totalPiercingValue = basePiercingValue * Value;
            piercingToAdd = totalPiercingValue - piercingToAdd;

            rangedWeapon.addPiercing(piercingToAdd);
        }
    }
    private void OnWeaponChanged(Weapon _weapon)
    {
        SwapStats(false);

        weapon = _weapon;
        rangedWeapon = weapon as RangedWeapon;

        SwapStats(true);
    }

    private void SwapStats(bool add)
    {
        int piercing = totalPiercingValue;
        if (!add)
        {
            piercing = -piercing;
        }

        if (rangedWeapon)
        {
            rangedWeapon.addPiercing(piercing);
        }
    }

    public float ExtraMeleeDamage()
    {
        return Value * baseExtraMeleeDamage;
    }

    public override void OnRemove()
    {
        SwapStats(false);
    }

    private void OnAttack(List<Enemy> hitEnemies)
    {
        if (hitEnemies.Count > 1)
        {
            foreach (Enemy enemy in hitEnemies)
            {
                float extraDamage = ExtraMeleeDamage() * (hitEnemies.Count - 1);
                enemy.TakeDamage(extraDamage);
            }
        }
    }
}
