using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour // Temporary script to handle some functionality
{
    [SerializeField] private float initialHealth, initialAttack, initialDefence, initialAttackSpeed, initialMovementSpeed;
    [SerializeField] private float initialMaxHealth;
    [SerializeField] Weapon initialWeapon;
    [SerializeField] List<Item> initialItems;

    ItemsManager itemsManager;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
        itemsManager = ItemsManager.Instance;
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
        if (CheckInvulnerable())
            return;
        playerData.Health -= amount - playerData.Defence; // Reduce damage by defence
        if (playerData.Health <= 0)
            PlayerDeath();
    }

    bool CheckInvulnerable()
    {
        Item olive = playerData.SearchForItem(407); // Olive
        if (olive)
        {
            OliveEffect oliveEffect = itemsManager.ActiveItems[olive].GetComponent<OliveEffect>();
            if (oliveEffect && oliveEffect.BarrierActive)
            {
                oliveEffect.BarrierActive = false;
                //Debug.Log("Barrier hit!");
                return true;
            }
        }
        return false;
    }
    
    private void PlayerDeath()
    {
        // Lose
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
