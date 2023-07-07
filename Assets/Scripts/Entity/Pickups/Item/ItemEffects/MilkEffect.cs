using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkEffect : ItemEffect
{
    float baseRangeValue = 3;
    float totalRangeValue;

    Weapon weapon;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        ValueChangedEvent += OnValueChanged; playerData.WeaponChangedEvent += OnWeaponChanged;

        weapon = playerData.Weapon;
        if (weapon)
        {
            totalRangeValue = baseRangeValue;

            SwapStats(true);
        }

    }
    private void OnValueChanged()
    {
        float rangeToAdd = totalRangeValue;
        totalRangeValue = baseRangeValue * Value;
        rangeToAdd = totalRangeValue - rangeToAdd;

        weapon.addRange(rangeToAdd);
    }
    private void OnWeaponChanged(Weapon _weapon)
    {
        SwapStats(false);

        weapon = _weapon;

        SwapStats(true);
    }

    private void SwapStats(bool add)
    {
        float range = totalRangeValue;
        if (!add)
        {
            range = -range;
        }
        weapon.addRange(range);
    }

    private void OnDestroy()
    {
        SwapStats(false);
    }
}
