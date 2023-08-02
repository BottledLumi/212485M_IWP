using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attributes", menuName = "Enemy Attributes")]
public class EnemyAttributes : ScriptableObject
{
    [SerializeField] private float maxHealth, attack, defence, attackSpeed, movementSpeed;
    public float getMaxHealth()
    {
        return maxHealth;
    }
    public float getAttack()
    {
        return attack;
    }
    public float getDefence()
    {
        return defence;
    }
    public float getAttackSpeed()
    {
        return attackSpeed;
    }
    public float getMovementSpeed()
    {
        return movementSpeed;
    }

}
