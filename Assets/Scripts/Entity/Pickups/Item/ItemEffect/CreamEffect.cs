using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CreamEffect", menuName = "ItemEffect/CreamEffect")]
public class CreamEffect : ItemEffect
{
    float baseMultiplier = 1f;
    int totalAtkValue;
    PlayerData playerData;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;

        ValueChangedEvent += OnValueChanged; playerData.DefenceChangedEvent += OnDefenceChanged;

        totalAtkValue = (int)(playerData.Defence * baseMultiplier);
        playerData.Attack += totalAtkValue;
    }
    private void OnValueChanged(int value)
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

    public override void OnRemove()
    {
        playerData.Attack -= totalAtkValue;
    }
}
