using UnityEngine;

public class EnemyObjective : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovementReference =
                collision.gameObject.GetComponent<EnemyMovement>();

            Interface.Instance.RemoveHealth(1);
            enemyMovementReference.TakeDamage(enemyMovementReference.maxEnemyHealthPoints);
        }
    }
}
