using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : MonoBehaviour
{
    ItemsManager itemsManager;

    GameObject player;

    [SerializeField] bool mobile;
    List<Enemy> hitEnemies = new List<Enemy>();
    private float totalAttack, totalRange, totalAttackSpeed, totalKnockback;

    Vector3 mousePosition;
    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalAttackSpeed, float _totalKnockback, GameObject _player)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalAttackSpeed = _totalAttackSpeed;
        totalKnockback = _totalKnockback;

        player = _player;
    }

    [HideInInspector] public Animator animator;

    private void Awake()
    {
        itemsManager = ItemsManager.Instance;

        animator = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); mousePosition.z = 0;

        float playerOffset = player.transform.localScale.y; // degree of weapon offset from player

        Vector3 weaponOffset = (mousePosition - player.transform.position).normalized * (totalRange / 2 + 0.5f * playerOffset);
        gameObject.transform.position = player.transform.position + weaponOffset;
        gameObject.transform.rotation = player.transform.rotation;
    }
    IEnumerator AttackCoroutine()
    {
        // theres some shyt bug where if i spam the goddamn attack too much eventually you can see a frame's worth of animation that shouldn't be there
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / animator.speed);
        gameObject.SetActive(false);
    }
    //private void Hit()
    //{
    //    //// Hitbox + SFX + VFX

    //    // Set up the parameters for the overlap check
    //    ContactFilter2D filter = new ContactFilter2D();
    //    filter.SetLayerMask(LayerMask.GetMask("Enemy")); // Set the layer mask to include only the enemy layer
    //    filter.useLayerMask = true;

    //    // Perform the overlap check
    //    Collider2D[] results = new Collider2D[10]; // Adjust the size based on the maximum number of expected enemies
    //    int numColliders = Physics2D.OverlapCollider(weaponCollider, filter, results);

    //    // Iterate through the colliders and check if they belong to enemy objects
    //    for (int i = 0; i < numColliders; i++)
    //    {
    //        Enemy enemy = results[i].GetComponent<Enemy>();
    //        if (enemy != null && !hitEnemies.Contains(enemy))
    //        {
    //            // Enemy detected, add it to the list
    //            hitEnemies.Add(enemy);
    //        }
    //    }

    //    // TODO: Perform actions on the detected enemies (e.g., deal damage, play sound effects, etc.)
    //    foreach (Enemy enemy in hitEnemies)
    //    {
    //        enemy.TakeDamage(totalAttack);
    //    }
    //}

    private void OnEnable()
    {
        animator.speed = (2.5f/totalAttackSpeed);
        transform.localScale = new Vector3(totalRange, totalRange, 0);

        //Hit();

        StartCoroutine(AttackCoroutine());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy && !hitEnemies.Contains(enemy))
                {
                    enemy.TakeDamage(totalAttack);
                    // Knockback
                    Vector2 knockbackDirection = other.transform.position - player.transform.position; // Calculate the knockback direction
                    knockbackDirection.Normalize(); // Normalize the direction vector to ensure consistent knockback speed
                    enemy.ApplyKnockback(knockbackDirection, totalKnockback);

                    hitEnemies.Add(enemy);
                }
                break;
        }
    }

    public void OnDisable()
    {
        SteakEffect steakEffect = itemsManager.SearchForItemEffect(409) as SteakEffect; // Steak
        if (steakEffect && hitEnemies.Count > 1)
        {
            foreach (Enemy enemy in hitEnemies)
            {
                float extraDamage = steakEffect.ExtraMeleeDamage() * (hitEnemies.Count-1);
                enemy.TakeDamage(extraDamage);
            }
        }
        hitEnemies.Clear();
    }
}
