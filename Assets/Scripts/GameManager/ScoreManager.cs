using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;


    public int correctSorts = 0;
    public int errors = 0;
    public float securityScore = 100f;
    public float environmentScore = 100f;

    public TMP_Text correctScoreText;
    public TMP_Text errorScoreText;

    private void Awake()
    {
        Instance = this;
        Debug.Log("ScoreManager READY");
    }

    public void RegisterSuccess()
    {
        correctSorts++;
        environmentScore += 2;
        correctScoreText.text = "Correct : " + correctSorts;
        Debug.Log("Succ�s de tri enregistr�");
    }

    public void RegisterError(string reason)
    {
        errors++;
        securityScore -= 10;
        environmentScore -= 5;
        errorScoreText.text = "Errors : " + errors;
        Debug.Log("Erreur :" + reason);
    }


}
