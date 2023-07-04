using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct PlayerStats
{
    public float health, attack, defence, attackSpeed, movementSpeed;
    public float maxHealth;

    public PlayerStats(float _health, float _attack, float _defence, float _attackSpeed, float _movementSpeed, float _maxHealth)
    {
        health = _health;
        attack = _attack;
        defence = _defence;
        attackSpeed = _attackSpeed;
        movementSpeed = _movementSpeed;
        maxHealth = _maxHealth;
    }
}
