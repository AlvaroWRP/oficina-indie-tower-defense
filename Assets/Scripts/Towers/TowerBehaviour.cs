using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public float attackDamage;
    public float attackSpeed;
    public GameObject projectile;
    float cooldown;
    GameObject target;
    public float attackRange;

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        float attackInterval = 1 / attackSpeed;

        if (cooldown >= attackInterval)
        {
            if (
                !target
                || Vector2.Distance(transform.position, target.transform.position) >= attackRange
            )
            {
                target = FindTarget();
            }
            else
            {
                GameObject projectileInstance = Instantiate(
                    projectile,
                    transform.position,
                    Quaternion.identity
                );
                projectileInstance.GetComponent<ProjectileBehaviour>().damage = attackDamage;
                projectileInstance.GetComponent<ProjectileBehaviour>().target = target.transform;

                cooldown = 0;
            }
        }
        else
        {
            cooldown += Time.deltaTime;
        }
    }

    GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) <= attackRange)
            {
                return enemy;
            }
        }
        return null;
    }
}
