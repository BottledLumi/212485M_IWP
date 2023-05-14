using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : ScriptableObject
{
    [SerializeField] protected Sprite weaponIcon;
    protected Sprite getWeaponIcon() { return weaponIcon; }

    [SerializeField] protected GameObject weaponAttack;
    public GameObject getWeaponAttack() { return weaponAttack; }
    protected List<GameObject> weaponAttackPool;

    [SerializeField] protected float attack;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected ushort magazineSize;
    [SerializeField] protected float range;
    [SerializeField] protected float knockback;
    abstract public List<GameObject> Attack(Vector3 userPos, Vector3 clickPos);
}
