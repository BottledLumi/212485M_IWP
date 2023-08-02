using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private float initialAttack, initialHealth, initialDefence, initialAttackSpeed, initialMovementSpeed;
    [SerializeField] private float initialMaxHealth;
    [SerializeField] Weapon initialWeapon;
    [SerializeField] List<Item> initialItems;

    public bool invincible { get; private set; }
    [HideInInspector] public bool canTakeDamage = true;
    [SerializeField] float invincibilityTimer;

    SpriteRenderer spriteRenderer;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InitialisePlayerStats();
    }

    public void Heal(float amount) {
        playerData.Health += amount;
        if (playerData.Health > playerData.MaxHealth)
            playerData.Health = playerData.MaxHealth;
    }

    public event System.Action HitEvent;
    public event System.Action<float> DamageTakenEvent;
    public void TakeDamage(float amount)
    {
        if (CheckInvulnerable())
            return;

        HitEvent?.Invoke();

        if (!canTakeDamage)
        {
            StartCoroutine(InvincibilityTimer());
            canTakeDamage = true;
            return;
        }

        float damage = amount - playerData.Defence;
        if (damage <= 0)
            damage = 1;

        playerData.Health -= damage; // Reduce damage by defence
        DamageTakenEvent?.Invoke(damage);
        if (playerData.Health <= 0)
            PlayerDeath();
        StartCoroutine(InvincibilityTimer());
    }

    bool CheckInvulnerable()
    {
        if (invincible)
            return true;
        return false;
    }

    IEnumerator InvincibilityTimer()
    {
        invincible = true;
        spriteRenderer.DOFade(0.1f, invincibilityTimer).SetEase(Ease.Flash, 8, 1);
        yield return new WaitForSeconds(invincibilityTimer);
        invincible = false;
    }
    
    private void PlayerDeath()
    {
        // Lose
        GameStateManager.instance.SetGameState(false);
    }

    private void InitialisePlayerStats()
    {
        PlayerStats playerStats = new PlayerStats(initialHealth, initialAttack, initialDefence, initialAttackSpeed, initialMovementSpeed, initialMaxHealth);
        playerData.InitBaseStats(playerStats);

        playerData.Weapon = initialWeapon;
        foreach (Item item in initialItems)
            playerData.AddItem(item);
    }
}
