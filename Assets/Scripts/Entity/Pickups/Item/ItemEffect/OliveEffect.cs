using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New OliveEffect", menuName = "ItemEffect/OliveEffect")]
public class OliveEffect : ItemEffect
{
    Player player;

    Transform VFX;
    GameObject barrierVFX;

    bool barrierActive = false;
    float baseTimeToActive = 40f;
    float timeToActive;
    float barrierAlpha = 40f / 255f;

    SpriteRenderer spriteRenderer;

    public override void OnAdd()
    {
        VFX = GameObject.Find("VFX").transform;

        barrierActive = true; timeToActive = baseTimeToActive;
        AddBarrier();
        player = ItemsManager.Instance.player;

        player.DamageTakenEvent += OnDamageTaken;

        ValueChangedEvent += OnValueChanged;
        BarrierActiveEvent += OnBarrierActive; BarrierInactiveEvent += OnBarrierInactive;
    }

    public override void Execute()
    {
        barrierVFX.transform.position = player.transform.position;
    }

    public override void OnRemove()
    {
        if (BarrierActive)
        {
            barrierActive = false;
            player.canTakeDamage = true;
        }

        spriteRenderer = null;
        Destroy(barrierVFX);

        player.DamageTakenEvent -= OnDamageTaken;
    }

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
                ItemsManager.Instance.StartCoroutine(BarrierTimerCoroutine());
                BarrierInactiveEvent?.Invoke();
            }
        }
    }
    public event System.Action BarrierActiveEvent, BarrierInactiveEvent;
    private void OnValueChanged(int value)
    {
        timeToActive = baseTimeToActive * Mathf.Pow(0.85f, Value-1); // Reduces barrier's time to active by 15%
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

    private void OnDamageTaken()
    {
        if (!player.canTakeDamage || !BarrierActive)
            return;

        BarrierActive = false;
        player.canTakeDamage = false;
    }

    private void AddBarrier()
    {
        barrierVFX = new GameObject();
        barrierVFX.transform.SetParent(VFX);

        spriteRenderer = barrierVFX.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load("ItemEffects/Barrier", typeof(Sprite)) as Sprite;
        spriteRenderer.color = new Color(160f / 255f, 200f / 255f, 150f / 255f, barrierAlpha);
        spriteRenderer.transform.localScale = new Vector3(3, 3, 3);
        spriteRenderer.sortingOrder = 3;
    }

    IEnumerator BarrierTimerCoroutine()
    {
        yield return new WaitForSeconds(timeToActive);
        BarrierActive = true;
    }
}
