using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : ScriptableObject
{
    protected int value;
    public event System.Action<int> ValueChangedEvent;
    public int Value
    {
        get { return value; }
        set { this.value = value; ValueChangedEvent?.Invoke(this.value); }
    }

    private void OnEnable()
    {
        value = 1;
    }

    public virtual void OnAdd()
    {
    }

    public virtual void OnRemove()
    {
    }

    public virtual void Execute()
    {
    }

    protected void SubscribeBuff(Buff buff)
    {
        buff.AssignValue(ValueChangedEvent, value);
    }
}