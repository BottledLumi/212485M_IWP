using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour // To manage item effects
{
    [SerializeField] GameObject items;

    private static ItemsManager instance;
    public static ItemsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemsManager>();

                if (instance == null)
                {
                    GameObject managerObject = new GameObject("ItemsManager");
                    instance = managerObject.AddComponent<ItemsManager>();
                }
            }
            return instance;
        }
    }

    PlayerData playerData;

    GameObject owner;
    public GameObject Owner{
        get { return owner; }
    }

    Dictionary<Item, GameObject> activeItems = new Dictionary<Item, GameObject>();
    public Dictionary<Item, GameObject> ActiveItems
    {
        get { return activeItems; }
    }

    private void Awake()
    {
        playerData = PlayerData.Instance;

        owner = GameObject.Find("Player");

        playerData.ItemAddedEvent += OnItemAdded;
        playerData.ItemRemovedEvent += OnItemRemoved;
    }

    private int CheckQuantity(Item item)
    {
        return playerData.Items[item];
    }

    private void OnItemAdded(Item item)
    {
        int itemQuantity = CheckQuantity(item);
        if (itemQuantity == 1) // If it is a new item
        {
            // Add item effect
            activeItems.Add(item, ItemToGameObject(item));
        }
        else
        {
            // Alter existing item effect
            ItemEffect itemEffect = activeItems[item].GetComponentInChildren<ItemEffect>();
            itemEffect.Value++;
        }
    }

    private void OnItemRemoved(Item item)
    {
        int itemQuantity = CheckQuantity(item);
        if (itemQuantity == 0) // If no more of the item
        {
            // Remove item effect
            GameObject.Destroy(activeItems[item]);
            activeItems.Remove(item);
        }
        else
        {
            // Reduce item effect
            ItemEffect itemEffect = activeItems[item].GetComponentInChildren<ItemEffect>();
            itemEffect.Value--;
        }
    }

    private GameObject ItemToGameObject(Item item)
    {
        GameObject gameObject = new GameObject();
        // Attach relevant item effect script
        switch (item.Index)
        {
            case 401: // Olive
                gameObject.AddComponent<OliveEffect>();
                gameObject.name = "Olive";
                // Recharges a barrier after 50s, barrier strength scales with amount of olives (every 3 is 1 hit)
                break;
            case 402: // Cream
                // Increase attack according to defence, ratio scales with amount of cream
                //playerStats.attack += (playerStats.defence * 1.5f);
                break;
            case 403: // Mashed Potato
                // Gain a stack of “Hot Potato” every 3 seconds of not taking DMG. Maximum of 4 stacks. Resets upon hit. Hot Potato: Increase ATK
                break;
            case 404: // Alfredo
                // Revive to full HP on death, removes an Alfredo
                break;
        }
        gameObject.transform.SetParent(items.transform);
        return gameObject;
    }

}
