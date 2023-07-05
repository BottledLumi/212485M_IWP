using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    Weapon weapon;
    List<GameObject> weaponPool;

    bool canAttack = true;
    float totalAttack, totalRange, totalAttackSpeed, totalKnockback;
    float totalBulletSpeed, totalBulletSize; // For ranged weapons only
    int totalMagazineSize, currentMagazineSize;
    float totalReloadTime = 1.4f;
    float autoAttackSpeed = 0.6f;
    bool isReloading = false;

    public event System.Action<int, int> MagazineChangedEvent;
    public event System.Action<float> ReloadEvent;

    float timeElapsed; // since last bullet

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        playerData.WeaponChangedEvent += OnWeaponChange;
    }
    void OnWeaponChange(Weapon _weapon)
    {
        if (weaponPool != null) // Destroy existing weapon pool
        {
            foreach (GameObject gameObject in weaponPool) 
                GameObject.Destroy(gameObject);
        }

        weapon = playerData.Weapon;
        InitWeaponPool();

        totalMagazineSize = weapon.getMagazineSize();
        currentMagazineSize = totalMagazineSize;
        CallMagazineChangedEvent();
    }
    private void Start()
    {
        weapon = playerData.Weapon;
        if (!weapon)
            return;
    }

    void Update()
    {
        if (!weapon)
            return;

        // Reload
        if (weapon.weaponType == Weapon.WEAPON_TYPE.RANGED)
        {
            if (currentMagazineSize == 0 || (currentMagazineSize < totalMagazineSize && timeElapsed > 3) || (currentMagazineSize < totalMagazineSize && Input.GetKey(KeyCode.R)))
            {
                if (!isReloading) // Reload
                {
                    ReloadEvent?.Invoke(totalReloadTime);
                    StartCoroutine(ReloadCoroutine());
                }
            }
        }

        // Attack input
        if (Input.GetMouseButton(0) && canAttack && currentMagazineSize > 0 && !isReloading
            && !(weapon.getAttackSpeed() > autoAttackSpeed && !Input.GetMouseButtonDown(0))) // Attack speed check for if weapon is automatic
        {
            GameObject activeWeapon = getInactiveWeapon();
            if (activeWeapon != null)
            {
                // Register item effects, weapon base stats, etc.
                totalAttack = weapon.getAttack() * playerData.Attack;
                totalRange = weapon.getRange();
                totalAttackSpeed = weapon.getAttackSpeed() * playerData.AttackSpeed;
                totalKnockback = weapon.getKnockback();

                StartCoroutine(AttackSpeedCoroutine());

                float playerOffset = gameObject.transform.localScale.y; // degree of weapon offset from player
                switch (weapon.weaponType)
                {
                    case Weapon.WEAPON_TYPE.MELEE:
                        {
                            MeleeWeaponAttack meleeWeaponAttack = activeWeapon.GetComponent<MeleeWeaponAttack>();
                            if (!meleeWeaponAttack)
                                break;
                            meleeWeaponAttack.SetAttackAttributes(totalAttack, totalRange, totalAttackSpeed, totalKnockback, gameObject);

                            activeWeapon.SetActive(true);
                            break;
                        }
                    case Weapon.WEAPON_TYPE.RANGED:
                        {
                            RangedWeaponAttack rangedWeaponAttack = activeWeapon.GetComponent<RangedWeaponAttack>();
                            if (!rangedWeaponAttack)
                                break;
                            RangedWeapon rangedWeapon = weapon as RangedWeapon;
                            if (rangedWeapon != null)
                            {
                                totalBulletSpeed = rangedWeapon.getBulletSpeed();
                                totalBulletSize = rangedWeapon.getBulletSize();
                            }
                            Vector3 mousePosition = Input.mousePosition;
                            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); mousePosition.z = 0;

                            Vector3 direction = new Vector3(
                                mousePosition.x - transform.position.x,
                                mousePosition.y - transform.position.y, 0).normalized;

                            rangedWeaponAttack.SetAttackAttributes(totalAttack, totalRange, totalKnockback, totalBulletSpeed, totalBulletSize, direction, gameObject);

                            currentMagazineSize--; // Reduce magazine size by 1
                            CallMagazineChangedEvent();

                            Vector3 weaponOffset = (mousePosition - transform.position).normalized * playerOffset;
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

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        if (Input.GetMouseButtonDown(0) && canAttack && currentMagazineSize > 0)
        {
            isReloading = false;
            yield break;
        }
        yield return new WaitForSeconds(totalReloadTime);
        currentMagazineSize = totalMagazineSize;
        CallMagazineChangedEvent();
        isReloading = false;
    }
    void CallMagazineChangedEvent()
    {
        MagazineChangedEvent?.Invoke(currentMagazineSize, totalMagazineSize);
    }
}
