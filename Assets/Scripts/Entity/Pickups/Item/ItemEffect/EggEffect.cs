using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EggEffect", menuName = "ItemEffect/EggEffect")]
public class EggEffect : ItemEffect
{
    int baseDefValue = 2;
    int totalDefValue;
    PlayerData playerData;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;
        totalDefValue = baseDefValue;

        ValueChangedEvent += OnValueChanged;

        totalDefValue = baseDefValue * Value;
        playerData.Defence += totalDefValue;
    }
    private void OnValueChanged(int value)
    {
        int DefToAdd = totalDefValue;
        totalDefValue = baseDefValue * Value;
        DefToAdd = totalDefValue - DefToAdd;

        playerData.Defence += DefToAdd;
    }

    public override void OnRemove()
    {
        playerData.Defence -= totalDefValue;
    }
}
