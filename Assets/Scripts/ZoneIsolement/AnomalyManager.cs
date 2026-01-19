using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance;

    public bool hasAnomaly = false;
    // this checks if the user has contained the leaking from the leaking container
     public bool leakContained = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ResetAnomaly()
    {
        hasAnomaly = false;
        leakContained = false;
    }
}
