using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] float initialContactDamage;
    float contactDamage;

    [SerializeField] bool scaleWithAtk;

    Enemy enemy;

    private void Start()
    {
        if (scaleWithAtk)
        {
            enemy = GetComponent<Enemy>();
            if (enemy)
            {
                contactDamage = enemy.Attack;
                enemy.AttackChangedEvent += OnAttackChanged;
            }
        }
        else
            contactDamage = initialContactDamage;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
            player.TakeDamage(contactDamage);
    }

    void OnAttackChanged(float _attack)
    {
        contactDamage = _attack;
    }
}
