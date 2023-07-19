using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyAttributes enemyAttributes;
    private int level;
    public int Level
    {
        get { return level; }
    }
    public float health { private set; get; }

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
    public float movementSpeed { private set; get; }

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

    [HideInInspector] private bool canAttack = false;

    public event System.Action<bool> CanAttackChangedEvent;
    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; CanAttackChangedEvent?.Invoke(canAttack); }
    }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody2D;

    AIPath aiPath;

    private void Awake()
    {
        itemsManager = ItemsManager.Instance;

        CanAttackChangedEvent += OnCanAttackChanged;
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
            GameSceneManager.instance.enemyKilled++;
        }
    }

    void OnCanAttackChanged(bool _canAttack)
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
        rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private IEnumerator DamageTakenIndicator()
    {
        spriteRenderer.color = Color.red;
        damageTaken = true;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.color = Color.white;
        damageTaken = false;
        rigidbody2D.velocity = Vector2.zero;
    }

    ItemsManager itemsManager;
    private void OnEnable()
    {
        level = 1;
        health = enemyAttributes.getHealth() * Mathf.Pow(1.2f, level-1);
        attack = enemyAttributes.getAttack() * Mathf.Pow(1.2f, level-1);
        defence = enemyAttributes.getDefence() * Mathf.Pow(1.2f, level-1);
        attackSpeed = enemyAttributes.getAttackSpeed();
        movementSpeed = enemyAttributes.getMovementSpeed();

        WaterEffect waterEffect = itemsManager.SearchForItemEffect(411) as WaterEffect;
        if (waterEffect) // Water
        {
            movementSpeed *= waterEffect.MovementSpeedMultiplier();
        }

        if (aiPath = GetComponent<AIPath>())
        {
            aiPath.maxSpeed = movementSpeed;
        }

        OnCanAttackChanged(canAttack);
    }
}
