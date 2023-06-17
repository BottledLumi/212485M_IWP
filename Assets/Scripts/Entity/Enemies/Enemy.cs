using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyAttributes enemyAttributes;
    private int level;
    private float health, attack, defence, attackSpeed, movementSpeed;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
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
    void Awake()
    {
        if (!target)
            target = GameObject.Find("Player");
        gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public bool IsDead()
    {
        return isDead;
    }

    private IEnumerator DamageTakenIndicator()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.3f);

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
