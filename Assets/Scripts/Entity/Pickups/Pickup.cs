using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName = "Pickup")]
public class Pickup : ScriptableObject
{
    [SerializeField] int index;
    public int Index
    {
        get { return index; }
    }


    [SerializeField] string itemName;
    public string ItemName
    {
        get { return itemName; }
    }


    [SerializeField] string description;
    public string Description
    {
        get { return description; }
    }


    [SerializeField] Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
    }
}
