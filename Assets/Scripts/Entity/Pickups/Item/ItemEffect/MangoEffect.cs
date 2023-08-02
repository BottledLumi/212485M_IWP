using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MangoEffect", menuName = "ItemEffect/MangoEffect")]
public class MangoEffect : ItemEffect
{
    float baseMaxHPValue = 10;
    float totalMaxHPValue = 0;

    PlayerData playerData;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;
        totalMaxHPValue = baseMaxHPValue;

        ValueChangedEvent += OnValueChanged;

        totalMaxHPValue = baseMaxHPValue * Value;
        playerData.MaxHealth += totalMaxHPValue;
    }
    private void OnValueChanged(int value)
    {
        float MaxHPToAdd = totalMaxHPValue;
        totalMaxHPValue = baseMaxHPValue * Value;
        MaxHPToAdd = totalMaxHPValue - MaxHPToAdd;

        playerData.MaxHealth += MaxHPToAdd;
    }

    public override void OnRemove()
    {
        playerData.MaxHealth -= totalMaxHPValue;
    }
}
