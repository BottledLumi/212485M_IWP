using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CheeseEffect", menuName = "ItemEffect/CheeseEffect")]
public class CheeseEffect : ItemEffect
{
    float baseAtkSpdMultiplier = 0.9f;
    float totalAtkSpdValue;

    PlayerData playerData;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;
        totalAtkSpdValue = baseAtkSpdMultiplier;

        ValueChangedEvent += OnValueChanged;

        totalAtkSpdValue = Mathf.Pow(baseAtkSpdMultiplier, Value);
        playerData.AttackSpeed *= totalAtkSpdValue;
    }
    private void OnValueChanged(int value)
    {
        playerData.AttackSpeed /= totalAtkSpdValue;
        totalAtkSpdValue = Mathf.Pow(baseAtkSpdMultiplier, Value);

        playerData.AttackSpeed *= totalAtkSpdValue;
    }

    public override void OnRemove()
    {
        playerData.AttackSpeed /= totalAtkSpdValue;
    }
}
