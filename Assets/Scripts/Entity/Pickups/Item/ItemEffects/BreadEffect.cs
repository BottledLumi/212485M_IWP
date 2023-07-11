using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadEffect : ItemEffect
{
    float baseExtraDamage = 3f;

    Weapon weapon;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        weapon = playerData.Weapon;
    }

    public float ExtraDamage()
    {
        return Value * baseExtraDamage;
    }
}
