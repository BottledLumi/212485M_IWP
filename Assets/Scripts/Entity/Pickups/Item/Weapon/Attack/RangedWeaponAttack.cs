using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAttack : MonoBehaviour
{
    ItemsManager itemsManager;

    GameObject player;

    List<Enemy> hitEnemies = new List<Enemy>();
    private float expireTime = 6;
    private float totalAttack, totalRange, totalKnockback;
    float totalBulletSpeed, totalBulletSize;
    int totalPiercing;
    Vector3 direction;

    float distanceTravelled = 0;

    private void Awake()
    {
        itemsManager = ItemsManager.Instance;
    }

    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalKnockback, float _totalBulletSpeed, float _totalBulletSize, int _totalPiercing, Vector3 _direction, GameObject _player)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalKnockback = _totalKnockback;
        totalBulletSpeed = _totalBulletSpeed;
        totalBulletSize = _totalBulletSize;
        totalPiercing = _totalPiercing;

        direction = _direction;

        player = _player;
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
        switch (other.gameObject.tag)
        {
            case "Enemy":
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy && !hitEnemies.Contains(enemy))
                {
                    enemy.TakeDamage(totalAttack);
                    // Knockback
                    Vector2 knockbackDirection = other.transform.position - player.transform.position; // Calculate the knockback direction
                    knockbackDirection.Normalize(); // Normalize the direction vector to ensure consistent knockback speed
                    enemy.ApplyKnockback(knockbackDirection, totalKnockback);

                    ItemEffectsOnEnemies(enemy);

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
        ItemEffects();

        distanceTravelled = 0;
        hitEnemies.Clear();
    }

    void ItemEffectsOnEnemies(Enemy enemy)
    {
        BreadEffect breadEffect = itemsManager.SearchForItemEffect(412) as BreadEffect; // Bread
        if (breadEffect)
        {
            float extraDamage = breadEffect.ExtraDamage() * enemy.Level;
            enemy.TakeDamage(extraDamage);
            Debug.Log(extraDamage);
        }
    }

    void ItemEffects()
    {
        CakeEffect cakeEffect = itemsManager.SearchForItemEffect(413) as CakeEffect; // Bread
        if (cakeEffect && hitEnemies.Count > 0)
        {
            float randomPercentage = Random.Range(0f, 1f);
            if (randomPercentage < cakeEffect.ProcChance())
                cakeEffect.CakeProc();
        }
    }
}
