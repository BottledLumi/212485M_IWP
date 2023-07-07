using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MangoEffect : ItemEffect
{
    float baseMaxHPValue = 10;
    float totalMaxHPValue;
    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
        totalMaxHPValue = baseMaxHPValue;

        ValueChangedEvent += OnValueChanged;

        totalMaxHPValue = baseMaxHPValue * Value;
        playerData.MaxHealth += totalMaxHPValue;
    }
    private void OnValueChanged()
    {
        float MaxHPToAdd = totalMaxHPValue;
        totalMaxHPValue = baseMaxHPValue * Value;
        MaxHPToAdd = totalMaxHPValue - MaxHPToAdd;

        playerData.MaxHealth += MaxHPToAdd;
    }

    private void OnDestroy()
    {
        playerData.MaxHealth -= totalMaxHPValue;
    }
}
