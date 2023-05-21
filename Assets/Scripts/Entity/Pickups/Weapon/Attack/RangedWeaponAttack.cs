using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAttack : MonoBehaviour
{
    List<GameObject> hitEnemies = new List<GameObject>();
    private float expireTime = 6;
    private float totalAttack, totalRange, totalAttackSpeed;
    float totalBulletSpeed, totalBulletSize;
    Vector3 direction;

    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalBulletSpeed, float _totalBulletSize, Vector3 _direction)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalBulletSpeed = _totalBulletSpeed;
        totalBulletSize = _totalBulletSize;
        direction = _direction;
    }

    //public Animator animator;
    PolygonCollider2D weaponCollider;

    private void Awake()
    {
        weaponCollider = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        transform.position += direction * totalBulletSpeed * Time.deltaTime;
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
