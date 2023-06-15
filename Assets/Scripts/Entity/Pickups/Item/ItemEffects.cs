using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemEffects
{
    public static PlayerData.PlayerStats AdjustStats(PlayerData.PlayerStats basePlayerStats)
    {
        Dictionary<Item, int> items = PlayerData.Instance.Items;
        PlayerData.PlayerStats playerStats = basePlayerStats;

        foreach (Item item in items.Keys)
        {
            switch (item.Index)
            {
                case 201: // Olive
                    playerStats.attack += 5f * items[item]; // Temporary item effect 
                    break;
            }
        }

        return playerStats;
    }
}
