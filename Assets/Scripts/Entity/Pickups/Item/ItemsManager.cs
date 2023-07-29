using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour // To manage all item effects
{
    // Singleton
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

    PlayerData playerData; // PlayerData Singleton

    public Player player { get; private set; } // Player

    List<Buff> activeBuffs = new List<Buff>();
    Dictionary<Item, ItemEffect> activeItemEffects = new Dictionary<Item, ItemEffect>();
    public Dictionary<Item, ItemEffect> ActiveItemEffects
    {
        get { return activeItemEffects; }
    }

    private void Awake()
    {
        playerData = PlayerData.Instance;

        player = GameObject.Find("Player").GetComponent<Player>();

        playerData.ItemAddedEvent += OnItemAdded;
        playerData.ItemRemovedEvent += OnItemRemoved;
    }

    private void Update()
    {
        foreach (ItemEffect itemEffect in activeItemEffects.Values)
            itemEffect.Execute();
    }

    public void AddBuff(Buff buff)
    {
        activeBuffs.Add(buff);
        StartCoroutine(buff.BuffCoroutine());
    }

    public void RemoveBuff(Buff buff)
    {
        activeBuffs.Remove(buff);
    }

    private void OnItemAdded(Item item)
    {
        int itemQuantity = CheckQuantity(item);
        if (itemQuantity == 1) // If it is a new item
        {
            // Add item effect
            activeItemEffects.Add(item, item.Effect);
            activeItemEffects[item].OnAdd();
        }
        else
        {
            // Alter existing item effect
            ItemEffect itemEffect = activeItemEffects[item];
            itemEffect.Value++;
        }
    }

    private void OnItemRemoved(Item item)
    {
        int itemQuantity = CheckQuantity(item);
        if (itemQuantity == 0) // If no more of the item
        {
            // Remove item effect
            activeItemEffects[item].OnRemove();
            activeItemEffects.Remove(item);
        }
        else
        {
            // Reduce item effect
            ItemEffect itemEffect = activeItemEffects[item];
            itemEffect.Value--;
        }
    }
    private int CheckQuantity(Item item)
    {
        return playerData.Items[item];
    }

    public ItemEffect SearchForItemEffect(string name)
    {
        foreach (Item item in activeItemEffects.Keys)
        {
            if (item.ItemName == name)
                return activeItemEffects[item];
        }
        return null;
    }

    public ItemEffect SearchForItemEffect(int index)
    {
        foreach (Item item in activeItemEffects.Keys)
        {
            if (item.Index == index)
                return activeItemEffects[item];
        }
        return null;
    }
}
