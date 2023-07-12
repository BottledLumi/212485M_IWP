using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakEffect : ItemEffect
{
    int basePiercingValue = 1;
    int totalPiercingValue;

    float baseExtraMeleeDamage = 4f;

    Weapon weapon;
    RangedWeapon rangedWeapon;
    private void Awake()
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
    private void OnValueChanged()
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

    private void OnDestroy()
    {
        SwapStats(false);
    }
}
