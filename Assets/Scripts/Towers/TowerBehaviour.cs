using UnityEngine;
using UnityEngine.InputSystem;

public class TowerBehaviour : MonoBehaviour
{
    public float attackDamage;
    public float attackSpeed;
    public int price;
    public GameObject projectile;
    float cooldown;
    GameObject target;
    public float attackRange;
    bool isBuying = true;
    int blockedCount;
    SpriteRenderer spriteRenderer;
    Color originalColor;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (!isBuying)
        {
            Shoot();
        }
        else
        {
            BuyingMode();
        }
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

    void BuyingMode()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Shop.Instance.shopObject.SetActive(true);
            Destroy(gameObject);
        }

        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = mousePosition;

        if (blockedCount != 0)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = originalColor;

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                int gold = ConvertGoldCounterTextToInt();

                if (gold - price < 0)
                {
                    return;
                }

                gold -= price;
                Interface.Instance.goldCounter.text = $"${gold}";

                isBuying = false;
                Shop.Instance.shopObject.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blocked") || collision.CompareTag("Tower"))
        {
            blockedCount++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Blocked") || collision.CompareTag("Tower"))
        {
            blockedCount--;
        }
    }

    int ConvertGoldCounterTextToInt()
    {
        string goldCounterText = Interface.Instance.goldCounter.text;
        return int.Parse(goldCounterText[1..]);
    }
}
