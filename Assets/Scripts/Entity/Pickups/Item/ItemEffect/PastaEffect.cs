using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PastaEffect", menuName = "ItemEffect/PastaEffect")]
public class PastaEffect : ItemEffect
{
    float baseHealthValue = 5f;

    PlayerData playerData;

    public override void OnAdd()
    {
        playerData = PlayerData.Instance;

        playerData.ItemAddedEvent += OnItemAdded;
    }

    public override void OnRemove()
    {
        playerData.ItemAddedEvent -= OnItemAdded;
    }

    void OnItemAdded(Item item)
    {
        ItemsManager.Instance.player.Heal(baseHealthValue * Value);
    }
}
