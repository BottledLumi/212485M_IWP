using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Ranged Weapon")]
public class RangedWeapon : Weapon
{
    void Awake()
    {
        for (int i = 0; i < magazineSize; i++)
        {
            GameObject newWeaponAttack = Instantiate(weaponAttack); newWeaponAttack.SetActive(false);
            weaponAttackPool.Add(newWeaponAttack);
        }
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
