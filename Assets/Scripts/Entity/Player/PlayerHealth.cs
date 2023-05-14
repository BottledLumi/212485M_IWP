using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private TMP_Text healthText;

    public void Start()
    {
        healthText = GameObject.Find("UI").transform.Find("PlayerHealth_Text").GetComponent<TMP_Text>();
        UpdateHealthText();
    }

    public void Heal(float amount) {
        health += amount;
        UpdateHealthText();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
            PlayerDeath();
        else
            UpdateHealthText();
    }

    
    private void PlayerDeath()
    {
        // Lose condition
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = "Health: " + health.ToString();
    }
}
