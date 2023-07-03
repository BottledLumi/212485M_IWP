using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyAttributes enemyAttributes;
    private int level;
    private float health, attack, defence, attackSpeed, movementSpeed;
    private bool isDead = false;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody2D;
    public float getHealth()
    {
        return health;
    }
    public float getAttack()
    {
        return attack;
    }
    public float getDefence()
    {
        return defence;
    }
    public float getAttackSpeed()
    {
        return attackSpeed;
    }
    public float getMovementSpeed()
    {
        return movementSpeed;
    }

    public GameObject target = null;
    void Start()
    {
        if (!target)
            target = GameObject.Find("Player");
        gameObject.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        if (health > 0)
            StartCoroutine(DamageTakenIndicator());
        health -= amount;
        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
        }
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public bool IsDead()
    {
        return isDead;
    }

    private IEnumerator DamageTakenIndicator()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.color = Color.white;
    }

    private void OnEnable()
    {
        level = 1;
        health = enemyAttributes.getHealth() * Mathf.Pow(1.2f, level-1);
        attack = enemyAttributes.getAttack() * Mathf.Pow(1.2f, level-1);
        defence = enemyAttributes.getDefence() * Mathf.Pow(1.2f, level-1);
        attackSpeed = enemyAttributes.getAttackSpeed();
        movementSpeed = enemyAttributes.getMovementSpeed();
    }
}
