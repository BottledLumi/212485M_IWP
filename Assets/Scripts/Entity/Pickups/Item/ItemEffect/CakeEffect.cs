using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CakeEffect", menuName = "ItemEffect/CakeEffect")]
public class CakeEffect : ItemEffect
{
    PlayerWeapon playerWeapon;

    float procChance = 0.5f;

    public override void OnAdd()
    {
        playerWeapon = ItemsManager.Instance.player.GetComponent<PlayerWeapon>();
        playerWeapon.AttackEvent += OnAttack;
    }
    private void CakeProc()
    {
        CakeBuff buff = CreateInstance<CakeBuff>();
        SubscribeBuff(buff);
        ItemsManager.Instance.AddBuff(buff);
    }

    private float ProcChance()
    {
        return procChance;
    }

    private void OnAttack(List<Enemy> hitEnemies)
    {
        if (hitEnemies.Count > 0)
        {
            float randomPercentage = Random.Range(0f, 1f);
            if (randomPercentage < ProcChance())
                CakeProc();
        }
    }
}

