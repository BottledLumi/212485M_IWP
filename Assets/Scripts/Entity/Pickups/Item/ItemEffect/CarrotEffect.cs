using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CarrotEffect", menuName = "ItemEffect/CarrotEffect")]
public class CarrotEffect : ItemEffect
{
    float baseAtkValue = 3;
    float totalAtkValue;

    PlayerData playerData;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;
        totalAtkValue = baseAtkValue;

        ValueChangedEvent += OnValueChanged;

        totalAtkValue = baseAtkValue * Value;
        playerData.Attack += totalAtkValue;
    }
    private void OnValueChanged(int value)
    {
        float atkToAdd = totalAtkValue;
        totalAtkValue = baseAtkValue * Value;
        atkToAdd = totalAtkValue - atkToAdd;

        playerData.Attack += atkToAdd;
    }

    public override void OnRemove()
    {
        playerData.Attack -= totalAtkValue;
    }
}
