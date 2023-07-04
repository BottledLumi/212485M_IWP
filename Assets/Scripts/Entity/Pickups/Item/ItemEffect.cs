using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    int value;
    public event System.Action ValueChangedEvent;
    public int Value
    {
        get { return value; }
        set { this.value = value; ValueChangedEvent?.Invoke(); }
    }
}