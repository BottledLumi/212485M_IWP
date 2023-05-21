using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : ScriptableObject
{
    public enum WEAPON_TYPE
    {
        MELEE,
        RANGED
    }
    [SerializeField] protected Sprite weaponIcon;
    protected Sprite getWeaponIcon() { return weaponIcon; }

    [SerializeField] protected GameObject weaponAttack;
    public GameObject getWeaponAttack() { return weaponAttack; }
    public abstract WEAPON_TYPE weaponType { get; }
    [SerializeField] protected float attack; public float getAttack() { return attack; }
    [SerializeField] protected float attackSpeed; public float getAttackSpeed() { return attackSpeed; }
    [SerializeField] protected ushort magazineSize; public ushort getMagazineSize() { return magazineSize; }
    [SerializeField] protected float range; public float getRange() { return range; }
    [SerializeField] protected float knockback; public float getKnockback() { return knockback; }
}
