using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedastal : MonoBehaviour
{
    Item item;
    [SerializeField] GameObject itemImage;

    private void Start()
    {
        ItemPool itemPool = GetComponent<ItemPool>();
        item = itemPool.Item;
        ItemIcon();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || !item)
            return;
        PlayerData playerData = PlayerData.Instance;
        if (item is Weapon)
        {
            Weapon tempWeapon = playerData.Weapon;
            playerData.Weapon = item as Weapon; item = tempWeapon;
        }
        else
        {
            playerData.AddItem(item);
            item = null;
        }
        ItemIcon();
    }

    void ItemIcon()
    {
        SpriteRenderer spriteRenderer = itemImage.GetComponent<SpriteRenderer>();
        if (spriteRenderer && item)
            spriteRenderer.sprite = item.Icon;
        else
            spriteRenderer.sprite = null;
    }
}
