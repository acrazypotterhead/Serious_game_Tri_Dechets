using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int correctSorts = 0;
    public int errors = 0;
    public float securityScore = 100f;
    public float environmentScore = 100f;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterSuccess()
    {
        correctSorts++;
        environmentScore += 2;
    }

    public void RegisterError(string reason)
    {
        errors++;
        securityScore -= 10;
        environmentScore -= 5;

        Debug.Log("Erreur :" + reason);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
