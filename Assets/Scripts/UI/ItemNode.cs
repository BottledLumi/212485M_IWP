using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNode : MonoBehaviour
{
    Cauldron cauldron;
    Item item;
    Player player;
    private void Awake()
    {
        cauldron = FindObjectOfType<Cauldron>(true);
        player = FindObjectOfType<Player>();
    }

    public void AddToCauldron()
    {
        if (Vector2.Distance(player.transform.position,cauldron.transform.position) < 5)
            cauldron.AddItem(item);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
