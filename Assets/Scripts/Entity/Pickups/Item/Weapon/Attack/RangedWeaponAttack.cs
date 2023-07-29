using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAttack : MonoBehaviour
{
    PlayerWeapon playerWeapon;


    List<Enemy> hitEnemies = new List<Enemy>();
    private float expireTime = 6;
    private float totalAttack, totalRange, totalKnockback;
    float totalBulletSpeed, totalBulletSize;
    int totalPiercing;
    Vector3 direction;

    float distanceTravelled = 0;

    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalKnockback, float _totalBulletSpeed, float _totalBulletSize, int _totalPiercing, Vector3 _direction, PlayerWeapon _playerWeapon)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalKnockback = _totalKnockback;
        totalBulletSpeed = _totalBulletSpeed;
        totalBulletSize = _totalBulletSize;
        totalPiercing = _totalPiercing;

        direction = _direction;

        playerWeapon = _playerWeapon;
    }

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
        switch (other.gameObject.tag)
        {
            case "Enemy":
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy && !hitEnemies.Contains(enemy))
                {
                    enemy.TakeDamage(totalAttack);
                    // Knockback
                    Vector2 knockbackDirection = other.transform.position - playerWeapon.transform.position; // Calculate the knockback direction
                    knockbackDirection.Normalize(); // Normalize the direction vector to ensure consistent knockback speed
                    enemy.ApplyKnockback(knockbackDirection, totalKnockback);

                    // Call enemy hit event
                    playerWeapon.CallEnemyHitEvent(enemy);

                    hitEnemies.Add(enemy);
                }
                if (totalPiercing > 0 && hitEnemies.Count >= totalPiercing)
                    gameObject.SetActive(false);
                break;
            case "Wall":
                gameObject.SetActive(false);
                break;
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
        if (playerWeapon) // Attack event
            playerWeapon.CallAttackEvent(hitEnemies);

        distanceTravelled = 0;
        hitEnemies.Clear();
    }
}
