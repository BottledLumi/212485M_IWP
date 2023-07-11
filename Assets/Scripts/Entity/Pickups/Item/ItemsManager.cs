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

    public ItemEffect SearchForItemEffect(string name)
    {
        foreach (Item item in activeItems.Keys)
        {
            if (item.ItemName == name)
                return activeItems[item].GetComponentInChildren<ItemEffect>();
        }
        return null;
    }

    public ItemEffect SearchForItemEffect(int index)
    {
        foreach (Item item in activeItems.Keys)
        {
            if (item.Index == index)
                return activeItems[item].GetComponentInChildren<ItemEffect>();
        }
        return null;
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
            case 401: // Carrot
                gameObject.AddComponent<CarrotEffect>();
                break;
            case 402: // Cheese
                gameObject.AddComponent<CheeseEffect>();
                break;
            case 403: // Egg
                gameObject.AddComponent<EggEffect>();
                break;
            case 404: // Flour
                gameObject.AddComponent<FlourEffect>();
                break;
            case 405: // Mango
                gameObject.AddComponent<MangoEffect>();
                break;
            case 406: // Milk
                gameObject.AddComponent<MilkEffect>();
                break;
            case 407: // Olive
                gameObject.AddComponent<OliveEffect>();
                break;
            case 408: // Potato
                gameObject.AddComponent<PotatoEffect>();
                break;
            case 409: // Steak
                gameObject.AddComponent<SteakEffect>();
                break;
            case 410: // Sugar
                gameObject.AddComponent<SugarEffect>();
                break;
            case 411: // Water
                gameObject.AddComponent<WaterEffect>();
                break;
            case 412: // Bread
                gameObject.AddComponent<MangoEffect>();
                break;
            case 413: // Cake
                gameObject.AddComponent<CakeEffect>();
                break;
            case 414: // Cream
                gameObject.AddComponent<CreamEffect>();
                break;
            case 415: // Pasta
                gameObject.AddComponent<CreamEffect>();
                break;
            case 416: // Carrot Cake
                gameObject.AddComponent<CreamEffect>();
                break;
            case 417: // French Toast
                gameObject.AddComponent<CreamEffect>();
                break;
            case 418: // Hamburger
                gameObject.AddComponent<CreamEffect>();
                break;
            case 419: // Mac & Cheese
                gameObject.AddComponent<CreamEffect>();
                break;
            case 420: // Mango Cake
                gameObject.AddComponent<CreamEffect>();
                break;
            case 421: // Mashed Potato
                // Gain a stack of “Hot Potato” every 3 seconds of not taking DMG. Maximum of 4 stacks. Resets upon hit. Hot Potato: Increase ATK
                break;
            case 422: // Alfredo
                // Revive to full HP on death, removes an Alfredo
                break;
            case 423: // Cheeseburger
                gameObject.AddComponent<CreamEffect>();
                break;
        }
        gameObject.name = item.ItemName;

        gameObject.transform.SetParent(items.transform);
        return gameObject;
    }

}
