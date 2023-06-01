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
            Vector3 direction = enemy.target.transform.position - transform.position;
            direction.Normalize(); // Normalize the direction vector to have a magnitude of 1

            // Move the enemy towards the player
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.Translate(direction * enemy.getMovementSpeed() * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || !canAttack)
            return;
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(enemy.getAttack());
            canAttack = false;
        }

        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        // theres some shyt bug where if i spam the goddamn attack too much eventually you can see a frame's worth of animation that shouldn't be there
        yield return new WaitForSeconds(enemy.getAttackSpeed());
        canAttack = true;
    }
}
