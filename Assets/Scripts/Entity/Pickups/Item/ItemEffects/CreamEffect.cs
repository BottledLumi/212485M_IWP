using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreamEffect : ItemEffect
{
    float baseMultiplier = 1f;
    int totalAtkValue;
    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;

        ValueChangedEvent += OnValueChanged; playerData.DefenceChangedEvent += OnDefenceChanged;

        totalAtkValue = (int)(playerData.Defence * baseMultiplier);
        playerData.Attack += totalAtkValue;
    }
    private void OnValueChanged()
    {
        int atkToAdd = totalAtkValue;
        totalAtkValue =(int)(playerData.Defence * (baseMultiplier + 0.5f * (Value-1)));
        atkToAdd = totalAtkValue - atkToAdd;

        playerData.Attack += atkToAdd;
    }

    private void OnDefenceChanged(float _defence)
    {
        int atkToAdd = totalAtkValue;
        totalAtkValue = (int)(_defence * (baseMultiplier + 0.5f * (Value - 1)));
        atkToAdd = totalAtkValue - atkToAdd;

        playerData.Attack += atkToAdd;
    }

    private void OnDestroy()
    {
        playerData.Attack -= totalAtkValue;
    }
}
