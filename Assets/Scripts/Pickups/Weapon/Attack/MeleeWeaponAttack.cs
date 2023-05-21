using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : MonoBehaviour
{
    [SerializeField] bool mobile;
    List<GameObject> hitEnemies = new List<GameObject>();
    private float totalAttack;
    private float totalRange;
    private float totalAttackSpeed;
    public void SetAttackAttributes(float _totalAttack, float _totalRange, float _totalAttackSpeed)
    {
        totalAttack = _totalAttack;
        totalRange = _totalRange;
        totalAttackSpeed = _totalAttackSpeed;
    }

    public Animator animator;
    PolygonCollider2D weaponCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponCollider = GetComponent<PolygonCollider2D>();
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / animator.speed);
        gameObject.SetActive(false);
    }
    private void Hit()
    {
        //// Hitbox + SFX + VFX
        // TODO: Add hit detection code here and add targets to the list
    }

    private void OnEnable()
    {
        animator.speed = totalAttackSpeed;
        transform.localScale = new Vector3(totalRange, totalRange, 0);

        Hit();

        StartCoroutine(AttackCoroutine());
    }

    public void OnDisable()
    {
        hitEnemies.Clear();
    }
}
