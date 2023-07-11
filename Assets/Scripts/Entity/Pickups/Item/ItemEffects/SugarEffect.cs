using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SugarEffect : ItemEffect
{
    float baseMsValue = 2;
    float totalMsValue;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
        totalMsValue = baseMsValue;

        ValueChangedEvent += OnValueChanged;

        totalMsValue = baseMsValue * Value;
        playerData.MovementSpeed += totalMsValue;
    }
    private void OnValueChanged()
    {
        float MsToAdd = totalMsValue;
        totalMsValue = baseMsValue * Value;
        MsToAdd = totalMsValue - MsToAdd;

        playerData.MovementSpeed += MsToAdd;
    }

    private void OnDestroy()
    {
        playerData.MovementSpeed -= totalMsValue;
    }
}
