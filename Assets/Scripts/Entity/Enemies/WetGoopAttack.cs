using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Dead script, to be removed
public class WetGoopAttack : MonoBehaviour
{
    Enemy enemy;
    AIDestinationSetter destinationSetter;
    AIPath aiPath; 
    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.target && !enemy.DamageTaken)
        {
            aiPath.enabled = true;
            // Move the enemy towards the player if enemy can attack
            if (enemy.canAttack)
            {
                destinationSetter.target = enemy.target.transform;
            }
            else
            {
                Vector2 direction = transform.position - enemy.target.transform.position;
                direction.Normalize();
                transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(direction.x, direction.y, 0), enemy.movementSpeed / 2 * Time.deltaTime);
                destinationSetter.target = transform;
            }
        }
        else
        {
            aiPath.enabled = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || !enemy.canAttack)
            return;
        
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(enemy.Attack);
            enemy.canAttack = false;
        }
        if (gameObject.activeInHierarchy)
            StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        if (!gameObject.activeInHierarchy)
            yield break;
        yield return new WaitForSeconds(enemy.attackSpeed);
        enemy.canAttack = true;
    }
}
