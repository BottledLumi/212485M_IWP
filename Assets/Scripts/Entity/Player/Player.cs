using UnityEngine;

public class Player : MonoBehaviour // Temporary script to handle some functionality
{
    [SerializeField] private float initialHealth, initialAttack, initialDefence, initialAttackSpeed, initialMovementSpeed;
    [SerializeField] private float initialMaxHealth;
    [SerializeField] Weapon initialWeapon;
    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
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

    public void TakeDamage(float amount)
    {
        playerData.Health -= amount - playerData.Defence; // Reduce damage by defence
        if (playerData.Health <= 0)
            PlayerDeath();
    }

    
    private void PlayerDeath()
    {
        // Lose
    }

    private void InitialisePlayerStats()
    {
        playerData.Health = initialHealth;
        playerData.Attack = initialAttack;
        playerData.Defence = initialDefence;
        playerData.AttackSpeed = initialAttackSpeed;
        playerData.MovementSpeed = initialMovementSpeed;

        playerData.MaxHealth = initialMaxHealth;

        playerData.Weapon = initialWeapon;
    }
}
