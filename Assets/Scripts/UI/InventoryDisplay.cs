using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
    }
    private void OnEnable()
    {
        
    }
}
