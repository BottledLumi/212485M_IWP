using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private Sprite weaponIcon;
    public Sprite getWeaponIcon() { return weaponIcon; }

    [SerializeField] private GameObject weaponAttack;
    public GameObject getWeaponAttack() { return weaponAttack; }

    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private ushort magazineSize;
    [SerializeField] private float range;
    [SerializeField] private float knockback;

    public List<GameObject> Attack(Vector3 userPos, Vector3 clickPos)
    {
        // Hitbox + SFX + VFX
        GameObject attackObject = Instantiate(weaponAttack, clickPos, Quaternion.identity);
        Animator attackAnimator = attackObject.GetComponent<Animator>();

        if (attackAnimator != null)
        {
            attackAnimator.SetTrigger("Attack");
        }
        // Wait until the animation has finished playing
        float animationLength = attackAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        // Destroy the attack object
        Destroy(attackObject);

        // return targets
        return null;
    }
}
