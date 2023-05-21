using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    List<GameObject> weaponPool;
    private void Start()
    {
        if (!weapon)
            return;
        InitWeaponPool();
    }

    void Update()
    {
        // Attack input
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); mousePosition.z = 0;

            GameObject activeWeapon = getInactiveWeapon();
            if (activeWeapon != null)
            {
                switch (weapon.weaponType)
                {
                    case Weapon.WEAPON_TYPE.MELEE:
                        MeleeWeaponAttack meleeWeaponAttack = activeWeapon.GetComponent<MeleeWeaponAttack>();
                        if (!meleeWeaponAttack)
                            break;
                        meleeWeaponAttack.SetAttackAttributes(weapon.getAttack(), weapon.getRange(), weapon.getAttackSpeed());

                        Vector3 weaponOffset = (mousePosition - transform.position).normalized * (weapon.getRange()/2+0.5f);
                        activeWeapon.transform.position = transform.position + weaponOffset;
                        activeWeapon.transform.rotation = transform.rotation;
                        activeWeapon.SetActive(true);
                        break;
                    case Weapon.WEAPON_TYPE.RANGED:
                        RangedWeaponAttack rangedWeaponAttack = activeWeapon.GetComponent<RangedWeaponAttack>();
                        if (!rangedWeaponAttack)
                            break;

                        break;
                }
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

    //IEnumerator AttackSpeedCoroutine()
    //{
        //yield return new WaitForSeconds(weapon.);
    //}
}
