using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : MonoBehaviour
{
    PlayerWeapon playerWeapon;

    [SerializeField] bool mobile;
    List<Enemy> hitEnemies = new List<Enemy>();
    private float totalAttack, totalRange, totalAttackSpeed, totalKnockback;

    Vector3 mousePosition;
    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalAttackSpeed, float _totalKnockback, PlayerWeapon _playerWeapon)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalAttackSpeed = _totalAttackSpeed;
        totalKnockback = _totalKnockback;

        playerWeapon = _playerWeapon;
    }

    [HideInInspector] public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); mousePosition.z = 0;

        float playerOffset = playerWeapon.transform.localScale.y; // degree of weapon offset from player

        Vector3 weaponOffset = (mousePosition - playerWeapon.transform.position).normalized * (totalRange / 2 + 0.5f * playerOffset);
        gameObject.transform.position = playerWeapon.transform.position + weaponOffset;
        gameObject.transform.rotation = playerWeapon.transform.rotation;
    }
    IEnumerator AttackCoroutine()
    {
        // theres some shyt bug where if i spam the goddamn attack too much eventually you can see a frame's worth of animation that shouldn't be there
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / animator.speed);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        animator.speed = (2.5f/totalAttackSpeed);
        transform.localScale = new Vector3(totalRange, totalRange, 0);

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
                    Vector2 knockbackDirection = other.transform.position - playerWeapon.transform.position; // Calculate the knockback direction
                    knockbackDirection.Normalize(); // Normalize the direction vector to ensure consistent knockback speed
                    enemy.ApplyKnockback(knockbackDirection, totalKnockback);

                    // Enemy hit event
                    playerWeapon.CallEnemyHitEvent(enemy);

                    hitEnemies.Add(enemy);
                }
                break;
        }
    }

    public void OnDisable()
    {
        if (playerWeapon) // Attack event
            playerWeapon.CallAttackEvent(hitEnemies);

        hitEnemies.Clear();
    }
}
