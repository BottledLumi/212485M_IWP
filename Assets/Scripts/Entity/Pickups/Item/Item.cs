using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : Pickup
{
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Mythic
    }
    [SerializeField] private ItemEffect itemEffect;
    public ItemEffect Effect
    {
        get { return itemEffect; }
    }

    [SerializeField] Rarity rarity;
    public Rarity itemRarity
    {
        get { return rarity; }
    }
        
}
