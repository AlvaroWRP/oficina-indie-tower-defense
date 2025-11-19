using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }
    public RectTransform background;
    public Button toggleButton;
    public Vector2 openedPosition;
    public Vector2 closedPosition;
    readonly float speed = 10;
    bool isOpen = false;
    public GameObject shopObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleBehaviour);
        background.anchoredPosition = closedPosition;
    }

    void Update()
    {
        MoveBackgroundPosition();
    }

    void ToggleBehaviour()
    {
        TextMeshProUGUI buttonTextComponent =
            toggleButton.GetComponentInChildren<TextMeshProUGUI>();

        isOpen = !isOpen;

        if (isOpen)
        {
            buttonTextComponent.text = "<";
        }
        else
        {
            buttonTextComponent.text = ">";
        }
    }

    void MoveBackgroundPosition()
    {
        Vector2 target = isOpen ? openedPosition : closedPosition;

        background.anchoredPosition = Vector2.Lerp(
            background.anchoredPosition,
            target,
            Time.deltaTime * speed
        );
    }

    public void SelectTower(GameObject towerPrefab)
    {
        Instantiate(towerPrefab, new Vector2(0, 0), Quaternion.identity);
        shopObject.SetActive(false);
    }
}
