using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetGoopAttack : MonoBehaviour
{
    Enemy enemy;
    bool canAttack = true;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.target)
        {
            // Calculate the direction from the enemy to the player
            Vector2 direction = enemy.target.transform.position - transform.position;
            direction.Normalize(); // Normalize the direction vector to have a magnitude of 1

            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction); //Rotate the enemy towards player

            // Move the enemy towards the player if enemy can attack
            if (canAttack)
                transform.position = Vector2.MoveTowards(transform.position, enemy.target.transform.position, enemy.getMovementSpeed() * Time.deltaTime);
            else
                transform.position = Vector2.MoveTowards(transform.position, -enemy.target.transform.position, enemy.getMovementSpeed()/2 * Time.deltaTime); //Run away if enemy can't attack
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || !canAttack)
            return;
        
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(enemy.getAttack());
            canAttack = false;
        }
        if (gameObject.activeInHierarchy)
            StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        if (!gameObject.activeInHierarchy)
            yield break;
        // theres some shyt bug where if i spam the goddamn attack too much eventually you can see a frame's worth of animation that shouldn't be there
        yield return new WaitForSeconds(enemy.getAttackSpeed());
        canAttack = true;
    }
}
