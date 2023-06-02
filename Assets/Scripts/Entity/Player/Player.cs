using UnityEngine;

public class Player : MonoBehaviour // Temporary script to handle some functionality
{
    [SerializeField] private float initialHealth, initialAttack, initialDefence, initialAttackSpeed, initialMovementSpeed;
    PlayerData playerData;
    private void Start()
    {
        playerData = PlayerData.Instance;
        InitialisePlayerStats();
    }
    public void Heal(float amount) {
        playerData.Health += amount;
    }

    public void TakeDamage(float amount)
    {
        playerData.Health -= amount;
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
    }
}
