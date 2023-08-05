using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    [SerializeField] private Slider reloadSlider;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText, attackText, defenceText, attackSpeedText, movementSpeedText;
    [SerializeField] private TMP_Text weaponText, magazineText;
    [SerializeField] private Image weaponImage;
    [SerializeField] PlayerWeapon playerWeapon;

    [SerializeField] private TMP_Text inventoryText;
    [SerializeField] private GameObject inventoryDisplay;

    [SerializeField] private GameObject itemPickupDisplay;

    [SerializeField] private GameObject bossHealthUI;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        playerData.InventoryChangedEvent += OnInventoryChanged;

        playerData.WeaponChangedEvent += OnWeaponChanged;

        playerData.HealthChangedEvent += OnHealthChanged;
        playerData.AttackChangedEvent += OnAttackChanged;
        playerData.DefenceChangedEvent += OnDefenceChanged;
        playerData.AttackSpeedChangedEvent += OnAttackSpeedChanged;
        playerData.MovementSpeedChangedEvent += OnMovementSpeedChanged;

        if (playerWeapon)
        {
            playerWeapon.MagazineChangedEvent += OnMagazineChanged;
            playerWeapon.ReloadEvent += OnReload;
        }

        playerData.ItemAddedEvent += OnItemAdded;
    }

    private void Start()
    {
        weaponImage.preserveAspect = true;

        GameStateManager.instance.BossEvent += OnBoss;
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
            newText += "\n" + item.ItemName + ":" + items[item];
        inventoryText.text = newText;
    }
    private void OnHealthChanged(float health, float maxHealth)
    {
        healthText.text = health.ToString()+"/"+maxHealth;
        healthSlider.maxValue = maxHealth; healthSlider.value = health;
    }
    private void OnAttackChanged(float attack)
    {
        attackText.text = "ATK: " + (attack * playerData.Weapon.getAttack()).ToString();
    }
    private void OnDefenceChanged(float defence)
    {
        defenceText.text = "DEF: " + defence.ToString();
    }
    private void OnAttackSpeedChanged(float attackSpeed)
    {
        attackSpeedText.text = "AS: " + (attackSpeed * playerData.Weapon.getAttackSpeed()).ToString() + "s";
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

        OnAttackChanged(playerData.Attack); OnAttackSpeedChanged(playerData.AttackSpeed);
    }

    [Header("Item Display Colours")]
    [SerializeField] Color commonPanel;
    [SerializeField] Color commonText;
    [SerializeField] Color uncommonPanel;
    [SerializeField] Color uncommonText;
    [SerializeField] Color rarePanel;
    [SerializeField] Color rareText;
    [SerializeField] Color mythicPanel;
    [SerializeField] Color mythicText;

    private void OnItemAdded(Item item)
    {
        GameObject display = GameObject.Instantiate(itemPickupDisplay);

        Image image = display.transform.Find("ItemImage").GetComponent<Image>();
        TMP_Text text = display.transform.Find("ItemDescription").GetComponent<TMP_Text>();
        RectTransform rectTransform = display.GetComponent<RectTransform>();

        if (image && text && display)
        {
            image.sprite = item.Icon;
            text.text = item.Description;

            switch (item.itemRarity)
            {
                case Item.Rarity.Common:
                    display.GetComponent<Image>().color = commonPanel;
                    text.color = commonText;
                    break;
                case Item.Rarity.Uncommon:
                    display.GetComponent<Image>().color = uncommonPanel;
                    text.color = uncommonText;
                    break;
                case Item.Rarity.Rare:
                    display.GetComponent<Image>().color = rarePanel;
                    text.color = rareText;
                    break;
                case Item.Rarity.Mythic:
                    display.GetComponent<Image>().color = mythicPanel;
                    text.color = mythicText;
                    break;
            }
        }

        display.transform.SetParent(UI.transform);
        display.GetComponent<RectTransform>().anchoredPosition = new Vector2(rectTransform.rect.width/2 + 20, rectTransform.rect.height/2 + 20);
    }

    GameObject bossUI;
    Slider bossSlider;
    void OnBoss(Boss boss)
    {
        bossUI = Instantiate(bossHealthUI, UI.transform);
        bossSlider = bossUI.transform.Find("BossHealth_Slider").GetComponent<Slider>();

        bossSlider.maxValue = boss.Health; bossSlider.value = boss.Health;
            
        boss.HealthChangedEvent += OnBossHealthChanged;
    }

    void OnBossHealthChanged(float _health)
    {
        bossSlider.value = _health;
        if (bossSlider.value <= 0)
        {
            bossSlider = null;
            Destroy(bossUI);
        }
    }
}
