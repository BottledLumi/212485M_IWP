using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private Sprite weaponIcon;
    public Sprite getWeaponIcon() { return weaponIcon; }

    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private ushort magazineSize;
    [SerializeField] private float range;
    [SerializeField] private float knockback;
}
