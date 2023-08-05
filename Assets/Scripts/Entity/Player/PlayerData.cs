using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    private static PlayerData instance;
    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance<PlayerData>();
            }
            return instance;
        }
    }

    private PlayerStats baseStats;
    private PlayerStats activeStats;
    public void InitBaseStats(PlayerStats _baseStats)
    {
        baseStats = _baseStats;
        ChangeStats(baseStats);
    }
    void ChangeStats(PlayerStats stats)
    {
        Health = stats.health; MaxHealth = stats.maxHealth;
        Attack = stats.attack;
        Defence = stats.defence;
        AttackSpeed = stats.attackSpeed;
        MovementSpeed = stats.movementSpeed;
    }

    Weapon weapon;
    public event System.Action<Weapon> WeaponChangedEvent;
    public Weapon Weapon
    {
        get { return weapon; }
        set
        {
            if (value != weapon)
            {
                weapon = value;
                WeaponChangedEvent?.Invoke(weapon);
            }
        }
    }

    private Dictionary<Item, int> items;
    public event System.Action InventoryChangedEvent;
    public event System.Action<Item> ItemAddedEvent, ItemRemovedEvent;
    public Dictionary<Item, int> Items
    {
        get { return items; }
    }
    public void AddItem(Item item)
    {
        if (items == null)
            items = new Dictionary<Item, int>();

        if (items.ContainsKey(item))
            items[item]++;
        else
            items.Add(item, 1);
        ItemAddedEvent?.Invoke(item);
        InventoryChangedEvent?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        if (!items.ContainsKey(item))
            return;
        items[item]--;
        ItemRemovedEvent?.Invoke(item);
        if (items[item] < 1)
            items.Remove(item);
        InventoryChangedEvent?.Invoke();
    }

    public Item SearchForItem(string name) // By name
    {
        foreach (Item item in items.Keys)
        {
            if (item.ItemName == name)
                return item;
        }
        return null;
    }

    public Item SearchForItem(int index) // By name
    {
        foreach (Item item in items.Keys)
        {
            if (item.Index == index)
                return item;
        }
        return null;
    }

    public event System.Action<float> AttackChangedEvent, DefenceChangedEvent, AttackSpeedChangedEvent, MovementSpeedChangedEvent;
    public event System.Action<float, float> HealthChangedEvent;
    public float Health
    {
        get { return activeStats.health; }
        set
        {
            if (value != activeStats.health)
            {
                activeStats.health = value;
                HealthChangedEvent?.Invoke(activeStats.health, activeStats.maxHealth);
            }
        }
    }

    public float Attack
    {
        get { return activeStats.attack; }
        set
        {
            if (value != activeStats.attack)
            {
                activeStats.attack = value;
                AttackChangedEvent?.Invoke(activeStats.attack);
            }
        }
    }
    public float Defence
    {
        get { return activeStats.defence; }
        set
        {
            if (value != activeStats.defence)
            {
                activeStats.defence = value;
                DefenceChangedEvent?.Invoke(activeStats.defence);
            }
        }
    }
    public float AttackSpeed
    {
        get { return activeStats.attackSpeed; }
        set
        {
            if (value != activeStats.attackSpeed)
            {
                activeStats.attackSpeed = value;
                AttackSpeedChangedEvent?.Invoke(activeStats.attackSpeed);
            }
        }
    }
    public float MovementSpeed
    {
        get { return activeStats.movementSpeed; }
        set
        {
            if (value != activeStats.movementSpeed)
            {
                activeStats.movementSpeed = value;
                MovementSpeedChangedEvent?.Invoke(activeStats.movementSpeed);
            }
        }
    }
    public float MaxHealth
    {
        get { return activeStats.maxHealth; }
        set
        {
            if (value != activeStats.maxHealth)
            {
                activeStats.maxHealth = value;
                HealthChangedEvent?.Invoke(activeStats.health, activeStats.maxHealth);
            }
        }
    }
}
