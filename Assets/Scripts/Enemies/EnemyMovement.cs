using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    int scenarioTurn = 0;
    public float maxEnemyHealthPoints = 5;
    float currentEnemyHealthPoints;
    public float enemySpeed;
    public bool isSlowed = false;

    void Start()
    {
        currentEnemyHealthPoints = maxEnemyHealthPoints;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 currentScenarioTurn = ScenarioTurns.Instance.scenarioTurns[scenarioTurn].position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            currentScenarioTurn,
            enemySpeed * Time.deltaTime
        );

        if (transform.position == currentScenarioTurn)
        {
            scenarioTurn++;
        }
    }

    public void TakeDamage(float damage)
    {
        currentEnemyHealthPoints -= damage;

        if (currentEnemyHealthPoints <= 0)
        {
            Destroy(gameObject);
            Waves.Instance.enemiesLeft--;
        }
    }
}
