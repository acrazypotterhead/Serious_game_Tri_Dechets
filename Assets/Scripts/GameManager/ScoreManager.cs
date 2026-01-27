using UnityEngine;
using System.Collections.Generic;
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
    public List<ErrorType> errorsMade = new List<ErrorType>();

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
        Debug.Log("Succès de tri enregistrés");
    }

    public void RegisterError(ErrorType errorType)
    {
        errors++;
        errorScoreText.text = "Errors : " + errors;

        if (!errorsMade.Contains(errorType))
            errorsMade.Add(errorType);

        Debug.Log("Erreur enregistrée : " + errorType);
    }

    public void RegisterEnvironmentError()
    {
        environmentScore -= 5;
    }

    public void RegisterSecurityError()
    {
        securityScore -= 5;
    }


}
