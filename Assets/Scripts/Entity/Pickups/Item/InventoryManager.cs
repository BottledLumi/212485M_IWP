using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour // To manage item effects
{
    PlayerData playerData;

    Dictionary<Item, int> activeItems;

    private void Awake()
    {
        playerData = PlayerData.Instance;

        playerData.InventoryChangedEvent += OnInventoryChanged;
    }

    private void OnInventoryChanged()
    {

    }

    //private void Compare
}
