using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAttack : MonoBehaviour
{
    List<Enemy> hitEnemies = new List<Enemy>();
    private float expireTime = 6;
    private float totalAttack, totalRange;
    float totalBulletSpeed, totalBulletSize;
    Vector3 direction;

    float distanceTravelled = 0;

    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalBulletSpeed, float _totalBulletSize, Vector3 _direction)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalBulletSpeed = _totalBulletSpeed;
        totalBulletSize = _totalBulletSize;
        direction = _direction;
    }

    //public Animator animator;
    //PolygonCollider2D weaponCollider;

    private void Update()
    {
        float distance = direction.magnitude * totalBulletSpeed * Time.deltaTime;
        // Add the distance to the total distance
        distanceTravelled += distance;
        if (distanceTravelled > totalRange)
            gameObject.SetActive(false);

        transform.position += direction * totalBulletSpeed * Time.deltaTime;
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(expireTime);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy && !hitEnemies.Contains(enemy))
            {
                enemy.TakeDamage(totalAttack);
                hitEnemies.Add(enemy);
            }
        }
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(totalBulletSize, totalBulletSize, 0);

        distanceTravelled = 0;
        StartCoroutine(AttackCoroutine());
    }

    public void OnDisable()
    {
        distanceTravelled = 0;
        hitEnemies.Clear();
    }
}
