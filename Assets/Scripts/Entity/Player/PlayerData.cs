using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    private static PlayerData instance;

    Weapon weapon;
    private float health, attack, defence, attackSpeed, movementSpeed;
    private float maxHealth;
    private Dictionary<Item, int> items;
    public event System.Action<Dictionary<Item, int>> InventoryChangedEvent;
    public Dictionary<Item, int> Items
    {
        get { return items; }
        set { items = value; InventoryChangedEvent?.Invoke(items); }
    }
    public void AddItem(Item item)
    {
        if (items == null)
            items = new Dictionary<Item, int>();

        if (items.ContainsKey(item))
            items[item]++;
        else
            items.Add(item, 1);
        InventoryChangedEvent?.Invoke(items);
    }

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
    public event System.Action<float> AttackChangedEvent, DefenceChangedEvent, AttackSpeedChangedEvent, MovementSpeedChangedEvent;
    public event System.Action<float, float> HealthChangedEvent;
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
    public float Health
    {
        get { return health; }
        set
        {
            if (value != health)
            {
                health = value;
                HealthChangedEvent?.Invoke(health, maxHealth);
            }
        }
    }

    public float Attack
    {
        get { return attack; }
        set
        {
            if (value != attack)
            {
                attack = value;
                AttackChangedEvent?.Invoke(attack);
            }
        }
    }
    public float Defence
    {
        get { return defence; }
        set
        {
            if (value != defence)
            {
                defence = value;
                DefenceChangedEvent?.Invoke(defence);
            }
        }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            if (value != attackSpeed)
            {
                attackSpeed = value;
                AttackSpeedChangedEvent?.Invoke(attackSpeed);
            }
        }
    }
    public float MovementSpeed
    {
        get { return movementSpeed; }
        set
        {
            if (value != movementSpeed)
            {
                movementSpeed = value;
                MovementSpeedChangedEvent?.Invoke(movementSpeed);
            }
        }
    }
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            if (value != maxHealth)
            {
                maxHealth = value;
                HealthChangedEvent?.Invoke(health, maxHealth);
            }
        }
    }
}
