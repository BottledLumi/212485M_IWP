using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    private static PlayerData instance;

    private float health, attack, defence, attackSpeed, movementSpeed;
    private List<Item> items;
    public List<Item> Items
    {
        get { return items; }
        set { items = value; }
    }
    public void AddItem(Item item)
    {
        items.Add(item);
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
    public event System.Action<float> HealthChangedEvent, AttackChangedEvent, DefenceChangedEvent, AttackSpeedChangedEvent, MovementSpeedChangedEvent;
    public float Health
    {
        get { return health; }
        set
        {
            if (value != health)
            {
                health = value;
                HealthChangedEvent?.Invoke(health);
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
}
