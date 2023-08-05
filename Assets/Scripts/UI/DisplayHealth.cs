using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    GameObject healthDisplay;
    Slider healthSlider;
    Enemy enemy;    

    void Start()
    {
        enemy = GetComponent<Enemy>();
        healthDisplay = Instantiate(Resources.Load("UI/HealthDisplay") as GameObject);
        healthDisplay.transform.localScale = transform.localScale;

        healthSlider = healthDisplay.GetComponent<Slider>();
        healthSlider.maxValue = enemy.MaxHealth;
        healthSlider.value = enemy.Health;

        healthDisplay.transform.SetParent(GameObject.Find("UI").transform);

        enemy.HealthChangedEvent += OnHealthChanged;
    }

    void LateUpdate()
    {
        Vector3 newPos = enemy.transform.position;
        newPos.y += transform.localScale.y * 0.75f;
        healthDisplay.transform.position = Camera.main.WorldToScreenPoint(newPos);
    }

    private void OnHealthChanged(float health)
    {
        healthSlider.value = health;
        if (health <= 0)
        {
            enemy.HealthChangedEvent -= OnHealthChanged;
            Destroy(healthDisplay);
            healthSlider = null;
        }
    }
}
