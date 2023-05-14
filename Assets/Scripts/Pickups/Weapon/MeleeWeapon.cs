using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Melee Weapon")]
public class MeleeWeapon : Weapon
{
    void Awake()
    {
        GameObject newWeaponAttack = Instantiate(weaponAttack); newWeaponAttack.SetActive(false);
    }

    override public List<GameObject> Attack(Vector3 userPos, Vector3 clickPos)
    {
        // Hitbox + SFX + VFX
        GameObject attackObject = Instantiate(weaponAttack, clickPos, Quaternion.identity);
        Animator attackAnimator = attackObject.GetComponent<Animator>();

        if (attackAnimator != null)
        {
            attackAnimator.SetTrigger("Attack");
        }
        List<GameObject> targets = new List<GameObject>();
        // TODO: Add hit detection code here and add targets to the list

        // return targets
        return targets;
    }
}
