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
    [SerializeField] Rarity rarity;
    public Rarity itemRarity
    {
        get { return rarity; }
    }
        
}
