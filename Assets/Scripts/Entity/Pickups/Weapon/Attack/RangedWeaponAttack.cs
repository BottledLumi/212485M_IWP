using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAttack : MonoBehaviour
{
    List<GameObject> hitEnemies = new List<GameObject>();
    private float expireTime = 10;
    private float totalAttack, totalRange, totalAttackSpeed;
    float totalBulletSpeed, totalBulletSize;

    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalBulletSpeed, float _totalBulletSize)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalBulletSpeed = _totalBulletSpeed;
        totalBulletSize = _totalBulletSize;
    }

    //public Animator animator;
    PolygonCollider2D weaponCollider;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        weaponCollider = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        transform.position += transform.forward * totalBulletSpeed;
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(expireTime);
        gameObject.SetActive(false);
    }
    private void Hit()
    {
        //// Hitbox + SFX + VFX
        // TODO: Add hit detection code here and add targets to the list
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(totalBulletSize, totalBulletSize, 0);

        Hit();

        StartCoroutine(AttackCoroutine());
    }

    public void OnDisable()
    {
        hitEnemies.Clear();
    }
}
