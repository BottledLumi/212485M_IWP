using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{
    public static PlayerStats AdjustStats(PlayerStats basePlayerStats) // For whole inventory
    {
        Dictionary<Item, int> items = PlayerData.Instance.Items;
        PlayerStats playerStats = basePlayerStats;

        foreach (Item item in items.Keys)
        {
            for (int i = 0; i < items[item]; i++)
                playerStats = RegisterEffect(playerStats, item);
        }
        return playerStats;
    }

    public static PlayerStats RegisterEffect(PlayerStats basePlayerStats, Item item)
    {
        PlayerStats playerStats = basePlayerStats;
        switch (item.Index)
        {
            case 401: // Olive
                playerStats.attack += 4f; // Temporary item effect 
                break;
            case 402: // Cream
                break;
            case 403: // Mashed Potato
                playerStats.attack += (playerStats.defence * 1.5f);
                break;
            case 404: // Alfredo
                break;
        }
        return playerStats;
    }
}
