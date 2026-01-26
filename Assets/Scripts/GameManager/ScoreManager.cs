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
        Debug.Log("ScoreManager READY");
    }

    public void RegisterSuccess()
    {
        correctSorts++;
        environmentScore += 2;
        Debug.Log("Succès de tri enregistré");
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
    void ShowDebugScores()
    {
        Debug.Log("---- SCORES ----");
        Debug.Log("Tri correct : " + correctSorts);
        Debug.Log("Erreurs : " + errors);
        Debug.Log("Sécurité : " + securityScore);
        Debug.Log("Environnement : " + environmentScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
