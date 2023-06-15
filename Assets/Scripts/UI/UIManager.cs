using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText, attackText, defenceText, attackSpeedText, movementSpeedText;
    [SerializeField] private TMP_Text weaponText, magazineText;
    [SerializeField] private Image weaponImage;
    [SerializeField] PlayerWeapon playerWeapon;

    [SerializeField] private TMP_Text inventoryText;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        playerData.InventoryChangedEvent += OnInventoryChanged;

        playerData.HealthChangedEvent += OnHealthChanged;
        playerData.AttackChangedEvent += OnAttackChanged;
        playerData.DefenceChangedEvent += OnDefenceChanged;
        playerData.AttackSpeedChangedEvent += OnAttackSpeedChanged;
        playerData.MovementSpeedChangedEvent += OnMovementSpeedChanged;

        playerData.WeaponChangedEvent += OnWeaponChanged;
        if (playerWeapon)
        {
            playerWeapon.MagazineChangedEvent += OnMagazineChanged;
            playerWeapon.ReloadEvent += OnReload;
        }
    }
    private void LateUpdate() // Late update for UI
    {
        if (reloadSlider.gameObject.activeInHierarchy)
        {
            Vector3 newPos = playerWeapon.gameObject.transform.position;
            newPos.y += 1.5f; // Position in world
            Vector3 newScreenPos = Camera.main.WorldToScreenPoint(newPos);
            RectTransform canvasRect = reloadSlider.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            Vector2 anchoredPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, newScreenPos, null, out anchoredPosition);
            reloadSlider.transform.localPosition = anchoredPosition;

            reloadSlider.value += Time.deltaTime;
            if (reloadSlider.value >= reloadSlider.maxValue)
                reloadSlider.gameObject.SetActive(false);
        }
    }
    private void OnInventoryChanged()
    {
        string newText = "Inventory:";
        Dictionary<Item, int> items = playerData.Items;
        foreach (Item item in items.Keys)
            newText += "\n" + item.name + ":" + items[item];
        inventoryText.text = newText;
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
    private void OnReload(float reloadTime)
    {
        reloadSlider.gameObject.SetActive(true);
        reloadSlider.maxValue = reloadTime;
        reloadSlider.value = 0;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        weaponText.text = weapon.name;
        weaponImage.sprite = weapon.Icon;
    }
}
