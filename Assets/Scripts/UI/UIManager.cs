using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText, attackText, defenceText, attackSpeedText, movementSpeedText;
    [SerializeField] private TMP_Text weaponText, magazineText;
    [SerializeField] private Image weaponImage;
    [SerializeField] PlayerWeapon playerWeapon;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        playerData.HealthChangedEvent += OnHealthChanged;
        playerData.AttackChangedEvent += OnAttackChanged;
        playerData.DefenceChangedEvent += OnDefenceChanged;
        playerData.AttackSpeedChangedEvent += OnAttackSpeedChanged;
        playerData.MovementSpeedChangedEvent += OnMovementSpeedChanged;

        playerData.WeaponChangedEvent += OnWeaponChanged;
        if (playerWeapon)
            playerWeapon.MagazineChangedEvent += OnMagazineChanged;
    }
    private void OnHealthChanged(float health, float maxHealth)
    {
        healthText.text = health.ToString()+"/"+maxHealth;
        healthSlider.maxValue = maxHealth; healthSlider.value = health;
    }
    private void OnAttackChanged(float attack)
    {
        attackText.text = "ATK: " + attack.ToString();
    }
    private void OnDefenceChanged(float defence)
    {
        defenceText.text = "DEF: " + defence.ToString();
    }
    private void OnAttackSpeedChanged(float attackSpeed)
    {
        attackSpeedText.text = "AS: " + attackSpeed.ToString();
    }
    private void OnMovementSpeedChanged(float movementSpeed)
    {
        movementSpeedText.text = "MS: " + movementSpeed.ToString();
    }
    private void OnMagazineChanged(int currentMagazineSize, int totalMagazineSize)
    {
        if (playerData.Weapon is MeleeWeapon)
            magazineText.text = "-";
        else
            magazineText.text = currentMagazineSize + "/" + totalMagazineSize;
    }
    private void OnWeaponChanged(Weapon weapon)
    {
        weaponText.text = weapon.name;
        weaponImage.sprite = weapon.Icon;
    }
}
