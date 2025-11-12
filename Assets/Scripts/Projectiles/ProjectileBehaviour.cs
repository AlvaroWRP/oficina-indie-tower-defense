using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float damage;
    public Transform target;
    public bool isCannon = false;
    public bool isSlow = false;
    public float radius;

    void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
        }
        MoveToTarget();
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            2 * Time.deltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyMovement enemyReference = collision.gameObject.GetComponent<EnemyMovement>();
            enemyReference.TakeDamage(damage);

            if (isCannon)
            {
                Collider2D[] enemiesInDamageRadius = Physics2D.OverlapCircleAll(
                    transform.position,
                    radius
                );

                foreach (Collider2D collision_ in enemiesInDamageRadius)
                {
                    if (collision_.CompareTag("Enemy"))
                    {
                        collision_.gameObject.GetComponent<EnemyMovement>().TakeDamage(damage);
                    }
                }
            }
            if (isSlow)
            {
                if (!enemyReference.isSlowed)
                {
                    enemyReference.enemySpeed *= 0.75f;
                    enemyReference.isSlowed = true;
                }
            }
            Destroy(gameObject);
        }
    }
}
