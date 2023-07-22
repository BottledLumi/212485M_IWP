using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float speed;

    [SerializeField] bool nonOwnerTarget;

    Vector3 direction;

    [SerializeField] bool activeProjectile;
    [SerializeField] List<GameObject> subProjectiles = new List<GameObject>();
    enum PROJECTILE_TYPE
    {
        SINGLE,
        BURST
    }
    [SerializeField] PROJECTILE_TYPE projectileType;

    [SerializeField] float burstSpeed;

    Enemy owner;
    float damage;

    private void Start()
    {
        if (!owner)
            return;
        transform.position = owner.transform.position;

        if (!nonOwnerTarget)
            target = owner.target;
        direction = target.transform.position - transform.position;
        direction.Normalize();
        transform.up = direction;

        damage = owner.Attack;

        switch (projectileType)
        {
            case PROJECTILE_TYPE.BURST:
                Vector3 initialPos = transform.position;
                StartCoroutine(StartBurst(initialPos));
                break;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.gameObject.GetComponent<Player>().TakeDamage(damage);
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(gameObject);
                break;
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetOwner(Enemy owner)
    {
        this.owner = owner;
    }

    IEnumerator StartBurst(Vector3 initialPos)
    {
        foreach (GameObject projectile in subProjectiles)
        {
            StartCoroutine(Burst(projectile, initialPos));
            // Wait before starting the next projectile burst
            yield return new WaitForSeconds(burstSpeed);
        }
    }

    IEnumerator Burst(GameObject projectile, Vector3 initialPos)
    {
        yield return new WaitForSeconds(burstSpeed);

        projectile.SetActive(true);
        projectile.transform.position = initialPos;
        EnemyProjectile enemyProjectile = projectile.GetComponent<EnemyProjectile>();
        if (!enemyProjectile.target)
            enemyProjectile.target = target;
        enemyProjectile.damage = damage;
    }
}
