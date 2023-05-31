using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : MonoBehaviour
{
    [SerializeField] bool mobile;
    List<Enemy> hitEnemies = new List<Enemy>();
    private float totalAttack, totalRange, totalAttackSpeed;
    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalAttackSpeed)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalAttackSpeed = _totalAttackSpeed;
    }

    [HideInInspector] public Animator animator;
    PolygonCollider2D weaponCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponCollider = GetComponent<PolygonCollider2D>();
    }
    IEnumerator AttackCoroutine()
    {
        // theres some shyt bug where if i spam the goddamn attack too much eventually you can see a frame's worth of animation that shouldn't be there
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / animator.speed);
        gameObject.SetActive(false);
    }
    private void Hit()
    {
        //// Hitbox + SFX + VFX

        // Set up the parameters for the overlap check
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemy")); // Set the layer mask to include only the enemy layer
        filter.useLayerMask = true;

        // Perform the overlap check
        Collider2D[] results = new Collider2D[10]; // Adjust the size based on the maximum number of expected enemies
        int numColliders = Physics2D.OverlapCollider(weaponCollider, filter, results);

        // Iterate through the colliders and check if they belong to enemy objects
        for (int i = 0; i < numColliders; i++)
        {
            Enemy enemy = results[i].GetComponent<Enemy>();
            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                // Enemy detected, add it to the list
                hitEnemies.Add(enemy);
            }
        }

        // TODO: Perform actions on the detected enemies (e.g., deal damage, play sound effects, etc.)
        foreach (Enemy enemy in hitEnemies)
        {
            enemy.TakeDamage(totalAttack);
        }
    }

    private void OnEnable()
    {
        animator.speed = (1/totalAttackSpeed);
        transform.localScale = new Vector3(totalRange, totalRange, 0);

        Hit();

        StartCoroutine(AttackCoroutine());
    }

    public void OnDisable()
    {
        hitEnemies.Clear();
    }
}
