using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyAttributes enemyAttributes;
    private int level;
    public int Level
    {
        get { return level; }
    }

    private float maxHealth;
    public event System.Action<float> MaxHealthChangedEvent;
    public float MaxHealth
    {
        get { return maxHealth; }
        private set
        {
            if (value != maxHealth)
            {
                maxHealth = value;
                MaxHealthChangedEvent?.Invoke(maxHealth);
            }
        }
    }

    private float health;
    public event System.Action<float> HealthChangedEvent;
    public float Health
    {
        get { return health; }
        private set
        {
            if (value != health)
            {
                health = value;
                HealthChangedEvent?.Invoke(health);
            }
        }
    }

    private float attack;
    public event System.Action<float> AttackChangedEvent;
    public float Attack
    {
        get { return attack; }
        private set {
            if (value != attack)
            {
                attack = value;
                AttackChangedEvent?.Invoke(attack);
            }
        }
    }
    public float defence { private set; get; }
    public float attackSpeed { private set; get; }

    private float movementSpeed;
    public event System.Action<float> MovementSpeedChangedEvent;
    public float MovementSpeed
    {
        get { return movementSpeed; }
        set
        {
            if (value != movementSpeed)
            {
                movementSpeed = value;
                MovementSpeedChangedEvent?.Invoke(movementSpeed);
            }
        }
    }

    [HideInInspector] public bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }
    [HideInInspector] public bool damageTaken = false;
    public bool DamageTaken
    {
        get { return damageTaken; }
    }

    [HideInInspector] protected bool canAttack = false;

    public event System.Action<bool> CanAttackChangedEvent;
    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; CanAttackChangedEvent?.Invoke(canAttack); }
    }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb2D;

    protected AIPath aiPath;

    private void Awake()
    {
        CanAttackChangedEvent += OnCanAttackChanged;
        MovementSpeedChangedEvent += OnMovementSpeedChanged;

        gameObject.SetActive(false);
    }

    [HideInInspector] public GameObject target = null;

    public void TakeDamage(float amount)
    {
        if (health > 0)
            StartCoroutine(DamageTakenIndicator());
        AudioController.Instance.PlaySound("EnemyHitSFX");
        Health -= amount;
        if (Health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
            GameStateManager.instance.enemyKilled++;
        }
    }

    protected void OnCanAttackChanged(bool _canAttack)
    {
        if (!_canAttack)
            StartCoroutine(AttackSpeedCoroutine());
    }

    IEnumerator AttackSpeedCoroutine()
    {
        yield return new WaitForSeconds(attackSpeed);
        CanAttack = true;
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        rb2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    float damageTakenTimer = 0.25f;
    private IEnumerator DamageTakenIndicator()
    {
        damageTaken = true;
        spriteRenderer.DOColor(Color.red, damageTakenTimer).SetEase(Ease.InCirc, 1, 2);
        yield return new WaitForSeconds(damageTakenTimer);
        damageTaken = false;
        spriteRenderer.color = Color.white;

        rb2D.velocity = Vector2.zero;
    }

    private void OnEnable()
    {
        InitBaseStats();

        if (aiPath = GetComponent<AIPath>())
        {
            aiPath.maxSpeed = movementSpeed;
        }

        OnCanAttackChanged(canAttack);
    }

    private void OnMovementSpeedChanged(float movementSpeed)
    {
        aiPath.maxSpeed = movementSpeed;
    }

    protected void InitBaseStats()
    {
        level = 1;
        maxHealth = enemyAttributes.getMaxHealth() * Mathf.Pow(1.2f, level - 1);
        health = maxHealth;
        attack = enemyAttributes.getAttack() * Mathf.Pow(1.2f, level - 1);
        defence = enemyAttributes.getDefence() * Mathf.Pow(1.2f, level - 1);
        attackSpeed = enemyAttributes.getAttackSpeed();
        movementSpeed = enemyAttributes.getMovementSpeed();
    }
}
