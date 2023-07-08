using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public override WEAPON_TYPE weaponType => WEAPON_TYPE.RANGED;
    public readonly float reloadSpeed = 1.4f;
    [SerializeField] private float bulletSpeed; public float getBulletSpeed() { return bulletSpeed; } public void addBulletSpeed(float _bulletSpeed) { bulletSpeed += _bulletSpeed; }
    [SerializeField] private float bulletSize; public float getBulletSize() { return bulletSize; } public void addBulletSize(float _bulletSize) { bulletSize += _bulletSize; }
    [SerializeField] private int piercing; public int getPiercing() { return piercing; } public void addPiercing(int _piercing) { piercing += _piercing; }
}
