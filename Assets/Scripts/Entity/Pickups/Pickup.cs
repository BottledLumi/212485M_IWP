using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName = "Pickup")]
public class Pickup : ScriptableObject
{
    [SerializeField] int index;
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
    }
}
