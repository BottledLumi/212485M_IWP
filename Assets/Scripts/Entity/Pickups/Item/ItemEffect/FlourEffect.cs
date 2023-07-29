using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FlourEffect", menuName = "ItemEffect/FlourEffect")]
public class FlourEffect : ItemEffect
{
    float baseRangeValue = 0.5f;
    float totalRangeValue;

    float baseBulletSizeValue = 0.5f;
    float totalBulletSizeValue;

    float baseBulletSpeedValue = 10f;
    float totalBulletSpeedValue;

    Weapon weapon;
    RangedWeapon rangedWeapon;

    PlayerData playerData;
    public override void OnAdd()
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
    private void OnValueChanged(int value)
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

    public override void OnRemove()
    {
        SwapStats(false);
    }
}
