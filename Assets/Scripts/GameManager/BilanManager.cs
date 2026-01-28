using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BilanManager : MonoBehaviour
{
    public static BilanManager Instance;

    [Header("Panel & Canvas")]
    public GameObject bilanPanel;
    public GameObject gameCanvas;

    [Header("Security")]
    public Slider securitySlider;
    public TMP_Text securityValueText;

    [Header("Environment")]
    public Slider environmentSlider;
    public TMP_Text environmentValueText;

    [Header("Other Infos")]
    public TMP_Text correctText;
    public TMP_Text errorText;
    public TMP_Text attentionText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bilanPanel.SetActive(false);

        securitySlider.minValue = 0;
        securitySlider.maxValue = 100;

        environmentSlider.minValue = 0;
        environmentSlider.maxValue = 100;
    }

    public void GameOver()
    {
        ShowResults();
        StartCoroutine(RestartAfterDelay(15f));
    }

    private void ShowResults()
    {
        var score = ScoreManager.Instance;

        gameCanvas.SetActive(false);
        bilanPanel.SetActive(true);
        

        // --- Security ---
        securitySlider.value = score.securityScore;
        securityValueText.text = score.securityScore.ToString("0");

        // --- Environment ---
        environmentSlider.value = score.environmentScore;
        environmentValueText.text = score.environmentScore.ToString("0");

        correctText.text = "Tri correct : " + score.correctSorts;
        errorText.text = "Erreurs : " + score.errors;

        GeneratePedagogicalFeedback(score);
    }

        private void GeneratePedagogicalFeedback(ScoreManager score)
    {
        attentionText.text = "";

        if (score.errorsMade.Count == 0)
        {
            attentionText.text = "No majors errors have been made during this session..";
            return;
        }

        foreach (var error in score.errorsMade)
        {
            switch (error)
            {
                case ErrorType.MissingEPI:
                    attentionText.text +=
                        "• Wear the EPI before entering work zone.\n";
                    break;

                case ErrorType.WrongSorting:
                    attentionText.text +=
                        "• Check the compatibility of the waste with the recycling bin.\n";
                    break;

                case ErrorType.UnverifiedAcid:
                    attentionText.text +=
                        "• Check the pH before making any sorting decisions.\n";
                    break;

                case ErrorType.Neutralize:
                    attentionText.text +=
                        "• Liquids with extreme pH needs to be neutralized first.\n";
                    break;

                case ErrorType.AccidentNotTreated:
                    attentionText.text +=
                        "• If accident happens, manage them in priority as they could cause others, or harm you.\n";
                    break;

                case ErrorType.WrongItemIsolated:
                    attentionText.text +=
                        "• Isolate only waste that presents an anomaly.\n";
                    break;
            }
        }
    }

    private IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reload la scène actuelle
        SceneManager.LoadScene(0);
    }

}
