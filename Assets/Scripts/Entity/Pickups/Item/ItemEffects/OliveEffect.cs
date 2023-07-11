using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OliveEffect : ItemEffect
{
    GameObject owner;

    bool barrierActive = false;
    float baseTimeToActive = 40f;
    float timeToActive;
    float barrierAlpha = 40f / 255f;

    SpriteRenderer spriteRenderer;
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
        barrierActive = true; timeToActive = baseTimeToActive;
        AddBarrier(); owner = ItemsManager.Instance.Owner;

        ValueChangedEvent += OnValueChanged;
        BarrierActiveEvent += OnBarrierActive; BarrierInactiveEvent += OnBarrierInactive;
    }
    private void OnValueChanged()
    {
        timeToActive = baseTimeToActive * Mathf.Pow(0.85f, Value-1); // Reduces barrier's time to active by 15%
        Debug.Log(timeToActive);
    }

    private void OnBarrierActive()
    {
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = barrierAlpha;
        spriteRenderer.color = spriteColor;
    }

    private void OnBarrierInactive()
    {
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 0f;
        spriteRenderer.color = spriteColor;
    }

    private void AddBarrier()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load("ItemEffects/Barrier", typeof(Sprite)) as Sprite;
        spriteRenderer.color = new Color(160f/255f, 200f/255f, 150f/255f, barrierAlpha);
        spriteRenderer.transform.localScale = new Vector3(3, 3, 3);
        spriteRenderer.sortingOrder = 3;
    }
    private void LateUpdate()
    {
        transform.position = owner.transform.position;
    }

    IEnumerator BarrierTimerCoroutine()
    {
        yield return new WaitForSeconds(timeToActive);
        BarrierActive = true;
    }

    private void OnDestroy()
    {

    }
}
