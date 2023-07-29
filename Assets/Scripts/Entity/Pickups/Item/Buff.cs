using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : ScriptableObject
{
    //Sprite sprite // Sprite for visual indicator

    protected float duration;
    protected int value;

    public virtual void BuffStart()
    {
    }
    public virtual void BuffEnd()
    {
    }

    public IEnumerator BuffCoroutine()
    {
        BuffStart();
        yield return new WaitForSeconds(duration);
        BuffEnd();
        Destroy(this);
    }

    public void AssignValue(System.Action<int> _event, int value)
    {
        this.value = value;
        _event += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        this.value = value;
    }
}
