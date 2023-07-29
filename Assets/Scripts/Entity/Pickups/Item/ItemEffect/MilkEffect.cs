using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MilkEffect", menuName = "ItemEffect/MilkEffect")]
public class MilkEffect : ItemEffect
{
    float baseRangeValue = 0.7f;
    float totalRangeValue;

    Weapon weapon;
    RangedWeapon rangedWeapon;

    PlayerData playerData;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;

        ValueChangedEvent += OnValueChanged; playerData.WeaponChangedEvent += OnWeaponChanged;

        weapon = playerData.Weapon;
        rangedWeapon = weapon as RangedWeapon;
        if (weapon)
        {
            totalRangeValue = baseRangeValue;

            SwapStats(true);
        }

    }
    private void OnValueChanged(int value)
    {
        float rangeToAdd = totalRangeValue;
        totalRangeValue = baseRangeValue * Value;
        rangeToAdd = totalRangeValue - rangeToAdd;

        if (rangedWeapon)
            rangeToAdd *= 6;
        weapon.addRange(rangeToAdd);
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
        float range = totalRangeValue;
        if (!add)
        {
            range = -range;
        }
        if (rangedWeapon)
            range *= 6;
        weapon.addRange(range);
    }

    public override void OnRemove()
    {
        SwapStats(false);
    }
}
