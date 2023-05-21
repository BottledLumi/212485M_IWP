using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Melee Weapon")]
public class MeleeWeapon : Weapon
{   
    public override WEAPON_TYPE weaponType => WEAPON_TYPE.MELEE;
}
