using UnityEngine;

public class IncidentManager : MonoBehaviour
{
    public static IncidentManager Instance;

    public float minTime = 30f;
    public float maxTime = 60f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke("TriggerIncident", Random.Range(minTime, maxTime));
    }

    public void TriggerIncident()
    {
        if (GameManager.Instance.currentState != GameState.Jeu)
            return;

        GameManager.Instance.ChangeState(GameState.Incident);
        Debug.Log("Incident déclenché");
    }
 

    // Update is called once per frame
    void Update()
    {
        
    }
}
