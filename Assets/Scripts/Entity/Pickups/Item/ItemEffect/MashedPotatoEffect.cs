using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MashedPotatoEffect", menuName = "ItemEffect/MashedPotatoEffect")]
public class MashedPotatoEffect : ItemEffect
{
    PlayerData playerData;
    ItemsManager itemsManager;
    Player player;

    uint stackCount = 0;
    float duration = 4f;

    float baseAttackValue = 3f;
    float totalAttackValue = 0;

    public override void OnAdd()
    {
        playerData = PlayerData.Instance;
        itemsManager = ItemsManager.Instance;
        player = ItemsManager.Instance.player;

        ValueChangedEvent += OnValueChanged;
        player.HitEvent += OnHit;

        currentCoroutine = itemsManager.StartCoroutine(StackCoroutine());
    }

    public override void OnRemove()
    {
        player.HitEvent -= OnHit;
    }

    Coroutine currentCoroutine;
    IEnumerator StackCoroutine()
    {
        yield return new WaitForSeconds(duration);
        stackCount++; StackAttack();
        if (stackCount < 4)
            currentCoroutine = itemsManager.StartCoroutine(StackCoroutine());
    }

    private void StackAttack()
    {
        playerData.Attack += baseAttackValue * Value;
        totalAttackValue += baseAttackValue * Value;
    }

    private void OnValueChanged(int value)
    {
        playerData.Attack -= totalAttackValue;
        playerData.Attack += baseAttackValue * Value * stackCount;
    }

    private void OnHit()
    {
        stackCount = 0;
        playerData.Attack -= totalAttackValue;
        itemsManager.StopCoroutine(currentCoroutine);
        currentCoroutine = itemsManager.StartCoroutine(StackCoroutine());
    }
}

