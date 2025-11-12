using System.Collections.Generic;
using UnityEngine;

public class ScenarioTurns : MonoBehaviour
{
    public static ScenarioTurns Instance { get; private set; }
    public List<Transform> scenarioTurns;

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
}
