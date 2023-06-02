using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    public void Heal(float amount) {
        playerData.health += amount;
    }

    public void TakeDamage(float amount)
    {
        playerData.health -= amount;
        Debug.Log(playerData.health);
        if (playerData.health <= 0)
            PlayerDeath();
    }

    
    private void PlayerDeath()
    {
        // Lose
    }
}
