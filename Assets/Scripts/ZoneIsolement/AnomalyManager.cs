using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance;

    public bool hasAnomaly = false;

    private void Awake()
    {
        Instance = this;
    }
}
