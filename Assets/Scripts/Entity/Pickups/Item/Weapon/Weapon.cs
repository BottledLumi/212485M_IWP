using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : Item
{
    public enum WEAPON_TYPE
    {
        MELEE,
        RANGED
    }

    [SerializeField] protected GameObject weaponAttack;
    public GameObject getWeaponAttack() { return weaponAttack; }
    public abstract WEAPON_TYPE weaponType { get; }
    [SerializeField] protected float attack; 
    public float getAttack() { return attack; } public void addAttack(float _attack) { attack += _attack; }


    [SerializeField] protected float attackSpeed; 
    public float getAttackSpeed() { return attackSpeed; } public void addAttackSpeed(float _attackSpeed) { attackSpeed += _attackSpeed; }


    [SerializeField] protected ushort magazineSize; 
    public ushort getMagazineSize() { return magazineSize; } public void addMagazineSize(float _magazineSize) { attack += _magazineSize; }


    [SerializeField] protected float range; 
    public float getRange() { return range; } public void addRange(float _range) { range += _range; }


    [SerializeField] protected float knockback; 
    public float getKnockback() { return knockback; } public void addKnockback(float _knockback) { knockback += _knockback; }

    [SerializeField] protected AudioClip soundEffect;
    public AudioClip getSoundEffect() { return soundEffect; }
}
