using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FrenchToastEffect", menuName = "ItemEffect/FrenchToastEffect")]
public class FrenchToastEffect : ItemEffect
{
    float baseDefValue = 1f;
    int totalDefValue;
    PlayerData playerData;
    public override void OnAdd()
    {
        playerData = PlayerData.Instance;

        ValueChangedEvent += OnValueChanged; playerData.MovementSpeedChangedEvent += OnMovementSpeedChanged;

        totalDefValue = (int)(playerData.MovementSpeed * baseDefValue);
        playerData.Defence += totalDefValue;
    }
    private void OnValueChanged(int value)
    {
        int defToAdd = totalDefValue;
        totalDefValue = (int)(playerData.MovementSpeed * (baseDefValue * (Value - 1)));
        defToAdd = totalDefValue - defToAdd;

        playerData.Defence += defToAdd;
    }

    private void OnMovementSpeedChanged(float _movementSpeed)
    {
        int defToAdd = totalDefValue;
        totalDefValue = (int)(playerData.MovementSpeed * (baseDefValue * (Value - 1)));
        defToAdd = totalDefValue - defToAdd;

        playerData.Defence += defToAdd;
    }

    public override void OnRemove()
    {
        playerData.Attack -= totalDefValue;
    }
}
