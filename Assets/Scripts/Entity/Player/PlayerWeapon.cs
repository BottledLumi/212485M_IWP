using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    List<GameObject> weaponPool;

    bool canAttack = true;
    float totalAttack, totalRange, totalAttackSpeed;
    float totalBulletSpeed, totalBulletSize; // For ranged weapons only
    private void Start()
    {
        if (!weapon)
            return;
        InitWeaponPool();
    }

    void Update()
    {
        // Attack input
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); mousePosition.z = 0;

            GameObject activeWeapon = getInactiveWeapon();
            if (activeWeapon != null)
            {
                // Register item effects, weapon base stats, etc.
                totalAttack = weapon.getAttack();
                totalRange = weapon.getRange();
                totalAttackSpeed = weapon.getAttackSpeed();

                StartCoroutine(AttackSpeedCoroutine());
                switch (weapon.weaponType)
                {
                    case Weapon.WEAPON_TYPE.MELEE:
                        {
                            MeleeWeaponAttack meleeWeaponAttack = activeWeapon.GetComponent<MeleeWeaponAttack>();
                            if (!meleeWeaponAttack)
                                break;
                            meleeWeaponAttack.SetAttackAttributes(totalAttack, totalRange, totalAttackSpeed);

                            Vector3 weaponOffset = (mousePosition - transform.position).normalized * (weapon.getRange() / 2 + 0.5f);
                            activeWeapon.transform.position = transform.position + weaponOffset;
                            activeWeapon.transform.rotation = transform.rotation;
                            activeWeapon.SetActive(true);
                            break;
                        }
                    case Weapon.WEAPON_TYPE.RANGED:
                        {
                            RangedWeaponAttack rangedWeaponAttack = activeWeapon.GetComponent<RangedWeaponAttack>();
                            if (!rangedWeaponAttack)
                                break;
                            rangedWeaponAttack.SetAttackAttributes(totalAttack, totalRange, totalBulletSpeed, totalBulletSize);

                            Vector3 weaponOffset = (mousePosition - transform.position).normalized * 0.5f;
                            activeWeapon.transform.position = transform.position + weaponOffset;
                            activeWeapon.transform.rotation = transform.rotation;
                            activeWeapon.SetActive(true);
                            break;
                        }
                }
                canAttack = false;
            }
        }
    }
    private GameObject getInactiveWeapon()
    {
        foreach (GameObject weapon in weaponPool)
        {
            if (!weapon.activeInHierarchy)
            {
                return weapon;
            }
        }
        return null;
    }
    private void InitWeaponPool()
    {
        weaponPool = new List<GameObject>();
        for (int i = 0; i < weapon.getMagazineSize(); i++)
        {
            GameObject weaponGO = Instantiate(weapon.getWeaponAttack()); weaponGO.SetActive(false);
            weaponPool.Add(weaponGO);
        }
    }

    IEnumerator AttackSpeedCoroutine()
    {
        yield return new WaitForSeconds(totalAttackSpeed);
        canAttack = true;
    }
}
