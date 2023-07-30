using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CarrotCakeEffect", menuName = "ItemEffect/CarrotCakeEffect")]
public class CarrotCakeEffect : ItemEffect
{
    float baseMultiplier = 0.99f;
    float totalAtkSpdValue;
    PlayerData playerData;

    public override void OnAdd()
    {
        playerData = PlayerData.Instance;

        ValueChangedEvent += OnValueChanged; playerData.AttackChangedEvent += OnAttackChanged;

        totalAtkSpdValue = AttackSpeedMultiplier();
        playerData.AttackSpeed *= totalAtkSpdValue;
    }
    private void OnValueChanged(int value)
    {
        playerData.AttackSpeed /= totalAtkSpdValue;
        playerData.AttackSpeed *= AttackSpeedMultiplier();
    }

    private void OnAttackChanged(float _attack)
    {
        playerData.AttackSpeed /= totalAtkSpdValue;
        playerData.AttackSpeed *= AttackSpeedMultiplier();
    }

    public override void OnRemove()
    {
        playerData.AttackSpeed /= totalAtkSpdValue;
    }

    private float AttackSpeedMultiplier()
    {
        return Mathf.Pow(baseMultiplier, playerData.Attack * Value);
    }
}
