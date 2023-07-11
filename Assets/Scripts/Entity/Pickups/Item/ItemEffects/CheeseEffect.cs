using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheeseEffect : ItemEffect
{
    float baseAtkSpdMultiplier = 0.9f;
    float totalAtkSpdValue;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
        totalAtkSpdValue = baseAtkSpdMultiplier;

        ValueChangedEvent += OnValueChanged;

        totalAtkSpdValue = Mathf.Pow(baseAtkSpdMultiplier, Value);
        playerData.AttackSpeed *= totalAtkSpdValue;
    }
    private void OnValueChanged()
    {
        playerData.AttackSpeed /= totalAtkSpdValue;
        totalAtkSpdValue = Mathf.Pow(baseAtkSpdMultiplier, Value);

        playerData.AttackSpeed *= totalAtkSpdValue;
    }

    private void OnDestroy()
    {
        playerData.AttackSpeed /= totalAtkSpdValue;
    }
}
