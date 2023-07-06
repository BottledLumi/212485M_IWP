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
            case 401: // Carrot
                gameObject.AddComponent<CarrotEffect>();
                gameObject.name = "Carrot";
                break;
            case 402: // Cheese
                gameObject.AddComponent<CheeseEffect>();
                gameObject.name = "Cheese";
                break;
            case 403: // Egg
                gameObject.AddComponent<EggEffect>();
                gameObject.name = "Egg";
                break;
            case 404: // Flour
                gameObject.AddComponent<EggEffect>();
                gameObject.name = "Flour";
                break;
            case 407: // Olive
                gameObject.AddComponent<OliveEffect>();
                gameObject.name = "Olive";
                break;
            case 414: // Cream
                gameObject.AddComponent<CreamEffect>();
                gameObject.name = "Cream";
                break;
            case 410: // Mashed Potato
                // Gain a stack of “Hot Potato” every 3 seconds of not taking DMG. Maximum of 4 stacks. Resets upon hit. Hot Potato: Increase ATK
                break;
            case 420: // Alfredo
                // Revive to full HP on death, removes an Alfredo
                break;
        }
        gameObject.transform.SetParent(items.transform);
        return gameObject;
    }

}
