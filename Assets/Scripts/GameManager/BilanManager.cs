using UnityEngine;

public class BilanManager : MonoBehaviour
{
    public void ShowResults()
    {
        Debug.Log("===== BILAN FINAL =====");

        Debug.Log("Tri correct :" + ScoreManager.Instance.correctSorts);
        Debug.Log("Erreurs :" + ScoreManager.Instance.errors);

        Debug.Log("Score Sécurité (ODD 3) :" + ScoreManager.Instance.securityScore);
        Debug.Log("Score Environnement (ODD 12) :" + ScoreManager.Instance.environmentScore);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
