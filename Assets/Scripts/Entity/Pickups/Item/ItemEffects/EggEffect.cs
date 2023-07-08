using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EggEffect : ItemEffect
{
    int baseDefValue = 2;
    int totalDefValue;
    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
        totalDefValue = baseDefValue;

        ValueChangedEvent += OnValueChanged;

        totalDefValue = baseDefValue * Value;
        playerData.Defence += totalDefValue;
    }
    private void OnValueChanged()
    {
        int DefToAdd = totalDefValue;
        totalDefValue = baseDefValue * Value;
        DefToAdd = totalDefValue - DefToAdd;

        playerData.Defence += DefToAdd;
    }

    private void OnDestroy()
    {
        playerData.Defence -= totalDefValue;
    }
}
