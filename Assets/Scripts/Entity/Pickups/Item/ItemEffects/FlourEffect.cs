using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourEffect : ItemEffect
{
    float baseRangeValue = 3;
    float totalRangeValue;

    float baseBulletSizeValue = 3;
    float totalBulletSizeValue;

    float baseBulletSpeedValue = 3;
    float totalBulletSpeedValue;

    Weapon weapon;
    RangedWeapon rangedWeapon;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        ValueChangedEvent += OnValueChanged; playerData.WeaponChangedEvent += OnWeaponChanged;

        weapon = playerData.Weapon;
        if (weapon)
        {
            rangedWeapon = weapon as RangedWeapon;

            totalRangeValue = baseRangeValue;
            totalBulletSizeValue = baseBulletSizeValue;
            totalBulletSpeedValue = baseBulletSpeedValue;

            SwapStats(true);
        }
        
    }
    private void OnValueChanged()
    {
        if (rangedWeapon)
        {
            float bulletSizeToAdd = totalBulletSizeValue;
            totalBulletSizeValue = baseBulletSizeValue * Value;
            bulletSizeToAdd = totalBulletSizeValue - bulletSizeToAdd;

            rangedWeapon.addBulletSize(bulletSizeToAdd);

            float bulletSpeedToAdd = totalBulletSpeedValue;
            totalBulletSpeedValue = baseBulletSpeedValue * Value;
            bulletSpeedToAdd = totalBulletSpeedValue - bulletSpeedToAdd;

            rangedWeapon.addBulletSpeed(bulletSpeedToAdd);
        }
        else
        {
            float rangeToAdd = totalRangeValue;
            totalRangeValue = baseRangeValue * Value;
            rangeToAdd = totalRangeValue - rangeToAdd;

            weapon.addRange(rangeToAdd);
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
        float bulletSize = totalBulletSizeValue, bulletSpeed = totalBulletSpeedValue, range = totalRangeValue;
        if (!add)
        {
            bulletSize = -bulletSize; bulletSpeed = -bulletSpeed; range = -range;
        }

        if (rangedWeapon)
        {
            rangedWeapon.addBulletSize(bulletSize);
            rangedWeapon.addBulletSpeed(bulletSpeed);
        }
        else
        {
            weapon.addRange(range);
        }
    }

    private void OnDestroy()
    {
        SwapStats(false);
    }
}
