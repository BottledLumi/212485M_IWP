using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SugarEffect", menuName = "ItemEffect/SugarEffect")]
public class SugarEffect : ItemEffect
{
    float baseMsValue = 2;
    float totalMsValue;

    PlayerData playerData;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;
        totalMsValue = baseMsValue;

        ValueChangedEvent += OnValueChanged;

        totalMsValue = baseMsValue * Value;
        playerData.MovementSpeed += totalMsValue;
    }
    private void OnValueChanged(int value)
    {
        float MsToAdd = totalMsValue;
        totalMsValue = baseMsValue * Value;
        MsToAdd = totalMsValue - MsToAdd;

        playerData.MovementSpeed += MsToAdd;
    }

    public override void OnRemove()
    {
        playerData.MovementSpeed -= totalMsValue;
    }
}
