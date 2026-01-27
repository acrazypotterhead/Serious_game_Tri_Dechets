using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;


    [Header("Scores")]
    public int correctSorts = 0;
    public int errors = 0;
    public float securityScore = 100f;
    public float environmentScore = 100f;

    [Header("Texts")]
    public TMP_Text correctScoreText;
    public TMP_Text errorScoreText;

    [Header("Sliders")]
    public Slider securitySlider;
    public Slider environmentSlider;

    [Header("Errors tracking")]
    public List<ErrorType> errorsMade = new List<ErrorType>();

    private void Awake()
    {
        Instance = this;
    }

     private void Start()
    {
        // Init sliders
        if (securitySlider != null)
        {
            securitySlider.minValue = 0f;
            securitySlider.maxValue = 100f;
            securitySlider.value = securityScore;
        }

        if (environmentSlider != null)
        {
            environmentSlider.minValue = 0f;
            environmentSlider.maxValue = 100f;
            environmentSlider.value = environmentScore;
        }

        UpdateUI();
    }

    /* =========================
     *  SUCCÈS
     * ========================= */
    public void RegisterSuccess()
    {
        correctSorts++;
        environmentScore += 2f;
        environmentScore = Mathf.Clamp(environmentScore, 0f, 100f);

        Debug.Log("Succès de tri enregistré");
        UpdateUI();
    }

    /* =========================
     *  ERREURS
     * ========================= */
    public void RegisterError(ErrorType errorType)
    {
        errors++;

        if (!errorsMade.Contains(errorType))
            errorsMade.Add(errorType);

        Debug.Log("Erreur enregistrée : " + errorType);
        UpdateUI();
    }

    public void RegisterSecurityError(int loss)
    {
        securityScore -= loss;
        securityScore = Mathf.Clamp(securityScore, 0f, 100f);

        Debug.Log("Sécurité - " + loss);
        UpdateUI();
    }

    public void RegisterEnvironmentError(int loss)
    {
        environmentScore -= loss;
        environmentScore = Mathf.Clamp(environmentScore, 0f, 100f);

        Debug.Log("Environnement - " + loss);
        UpdateUI();
    }
    /* =========================
     *  UI UPDATE
     * ========================= */
    private void UpdateUI()
    {
        if (correctScoreText != null)
            correctScoreText.text = "Correct : " + correctSorts;

        if (errorScoreText != null)
            errorScoreText.text = "Errors : " + errors;

        if (securitySlider != null)
            securitySlider.value = securityScore;

        if (environmentSlider != null)
            environmentSlider.value = environmentScore;
    }


}
