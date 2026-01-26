using UnityEngine;

public class IncidentManager : MonoBehaviour
{
    public static IncidentManager Instance;

    public float minTime = 5f;
    public float maxTime = 10f;
    private bool incidentActive = false;

    private void Awake()
    {
        Instance = this;
        Debug.Log("IncidentManager READY");
    }

    private void Start()
    {
        Invoke("TriggerIncident", Random.Range(minTime, maxTime));
    }

    public void TriggerIncident()
    {
        if (incidentActive)
            return; 

        if (GameManager.Instance.currentState != GameState.Jeu)
            return;

        incidentActive = true;

        GameManager.Instance.ChangeState(GameState.Incident);
        Debug.Log("Incident déclenché");
    }
 

    // Update is called once per frame
    void Update()
    {
        
    }
}
