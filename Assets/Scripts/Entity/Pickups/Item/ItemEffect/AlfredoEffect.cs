using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AlfredoEffect", menuName = "ItemEffect/AlfredoEffect")]
public class AlfredoEffect : ItemEffect
{
    PlayerData playerData;
    Player player;

    public override void OnAdd()
    {
        playerData = PlayerData.Instance;
        player = ItemsManager.Instance.player;

        player.DamageTakenEvent += OnDamageTaken;
    }
    public override void OnRemove()
    {
        player.DamageTakenEvent -= OnDamageTaken;
    }

    private void OnDamageTaken(float damage)
    {
        if (playerData.Health > 0)
            return;

        playerData.Health = playerData.MaxHealth;
        Value--;
        if (Value < 1)
            playerData.RemoveItem(ItemsManager.Instance.SearchForItem(this));
    }
}
