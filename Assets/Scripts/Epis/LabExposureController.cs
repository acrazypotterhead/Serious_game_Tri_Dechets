using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class LabExposureController : MonoBehaviour
{
    public static LabExposureController Instance;

    [Header("Dependencies")]
    public EPIManager epiManager;
    public ScoreManager scoreManager;

    [Header("UI")]
    public Slider exposureSlider;
    public TMP_Text exposureText;

    [Header("Exposure Settings")]
    public float exposureLevel = 0f;
    public float exposureIncreasePerSecond = 5f;
    public float maxExposure = 100f;

    [Header("Audio")]
    public AudioSource alarmSource;
    public AudioSource epiSelectedSource;
        public AudioClip allEpiSelectedClip;

    private bool exposureActive = false;
    private bool errorRegistered = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        exposureSlider.gameObject.SetActive(false);
        exposureSlider.maxValue = maxExposure;
        exposureSlider.value = 0f;

        if (alarmSource != null)
            alarmSource.loop = true;
            
    }

    private void Update()
    {
        if (!exposureActive) return;

        exposureLevel += exposureIncreasePerSecond * Time.deltaTime;
        exposureSlider.value = exposureLevel;

        if (exposureLevel >= maxExposure)
        {
            TriggerCriticalIncident();
        }
    }

    // this function is called when user enters lab WITHOUT EPI
    public void StartExposure()
    {
        if (exposureActive) return;

        exposureActive = true;
        exposureSlider.gameObject.SetActive(true);

        exposureText.text =  "No EPI detected : Chemical exposure in progress.";
        exposureText.color = Color.red;

        if (alarmSource != null && !alarmSource.isPlaying)
            alarmSource.Play();

        // Register ONE error only
        if (!errorRegistered)
        {
            errorRegistered = true;
            scoreManager.RegisterError("Entered zone without EPI.");
        }
    }

    public void PauseExposure()
    {
        exposureActive = false;

        if (alarmSource != null && alarmSource.isPlaying)
            alarmSource.Stop();

        exposureText.gameObject.SetActive(false);
        exposureSlider.gameObject.SetActive(false);
    }
    // resets exposure when EPIs are worn
    public void ResetExposure()
    {
        exposureActive = false;
        exposureLevel = 0f;
        exposureSlider.value = 0f;
        exposureSlider.gameObject.SetActive(false);

        if (alarmSource != null && alarmSource.isPlaying)
            alarmSource.Stop();

        errorRegistered = false;
        Debug.Log("All EPIs have been selected!");
        if (exposureText != null)
        {
            exposureText.gameObject.SetActive(true);
            exposureText.text = "ALL EPIs have been selected!";
            exposureText.color = Color.green;
            StartCoroutine(HideMessageAfterDelay());
            epiSelectedSource.PlayOneShot(allEpiSelectedClip);
        }
    }

     IEnumerator HideMessageAfterDelay() 
    { 
        yield return new WaitForSeconds(4f); 
        exposureText.gameObject.SetActive(false);
    }

    // Called when EPI are fully worn
    public void StopExposure()
    {
        exposureActive = false;
        exposureLevel = 0f;
        exposureSlider.value = 0f;
        exposureSlider.gameObject.SetActive(false);

        if (alarmSource != null && alarmSource.isPlaying)
            alarmSource.Stop();

        errorRegistered = false;
    }

    private void TriggerCriticalIncident()
    {
        exposureActive = false;

        if (alarmSource != null)
            alarmSource.Stop();

        Debug.Log("gameOver");

        scoreManager.RegisterError("Incident critique : exposition prolongée");

        exposureSlider.gameObject.SetActive(false);
        exposureLevel = 0f;
        exposureSlider.value = 0f;

        SceneManager.LoadScene("SceneWithAllInteraction");
    }

}
