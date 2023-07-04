using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OliveEffect : ItemEffect
{
    bool barrierActive = false;
    float timeToActive = 20f;
    public bool BarrierActive
    {
        get { return barrierActive; }
        set
        {
            barrierActive = value;
            if (barrierActive)
                BarrierActiveEvent?.Invoke();
            else
            {
                StartCoroutine(BarrierTimerCoroutine());
                BarrierInactiveEvent?.Invoke();
            }
        }
    }
    public event System.Action BarrierActiveEvent, BarrierInactiveEvent;
    private void Awake()
    {
        barrierActive = true;
    }

    IEnumerator BarrierTimerCoroutine()
    {
        yield return new WaitForSeconds(timeToActive);
        BarrierActive = true;
        Debug.Log("Barrier active");
    }


    private void OnDestroy()
    {

    }
}
