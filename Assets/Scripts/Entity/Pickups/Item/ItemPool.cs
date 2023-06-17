using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] List<Item> itemPool;
    [SerializeField] Item item;
    public Item Item
    {
        get { return item; }
    }

    [SerializeField] float commonChance, uncommonChance, rareChance, mythicChance;
    private void Awake()
    {
        if (!item)
        {
            float randomPercentage = Random.Range(0f, 1f) * 100f;
            List<Item> itemsOfRarity;
            if (randomPercentage < commonChance)
                itemsOfRarity = SearchForRarity(Item.Rarity.Common);
            else if (randomPercentage < commonChance + uncommonChance)
                itemsOfRarity = SearchForRarity(Item.Rarity.Uncommon);
            else if (randomPercentage < commonChance + uncommonChance + rareChance)
                itemsOfRarity = SearchForRarity(Item.Rarity.Rare);
            else 
                itemsOfRarity = SearchForRarity(Item.Rarity.Mythic);
            //Debug.Log("Chance: " + commonChance + uncommonChance + rareChance + "Got: " + randomPercentage);
            if (itemsOfRarity.Count > 0)
            {
                int randomIndex = Random.Range(0, itemsOfRarity.Count); // Generate a random index
                item = itemsOfRarity[randomIndex]; // Access the element at the random index
                Debug.Log("item found: " + item.name);
            }
        }
    }

    List<Item> SearchForRarity(Item.Rarity rarity)
    {
        List<Item> itemsOfRarity = new List<Item>();
        foreach (Item item in itemPool)
        {
            if (item.itemRarity == rarity)
                itemsOfRarity.Add(item);
        }
        return itemsOfRarity;
    }
}
