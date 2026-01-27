using UnityEngine;
using TMPro;
using System.Collections;

public class EPIManager : MonoBehaviour
{
    public static EPIManager Instance { get; private set; }

    public int selectedEPIIndex = 0;
    public int totalEPI = 3;
    public TMP_Text epiCounterText;
    public TMP_Text epiText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip allEpiSelectedClip;

    private bool allEpiDone = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        selectedEPIIndex = 0;
        if (epiCounterText != null)
        {
            epiCounterText.gameObject.SetActive(true);
            UpdateEpiText();
        }
    }


    public void RegisterEPISelected()
    {
        selectedEPIIndex++;
        Debug.Log($"EPI selected: {selectedEPIIndex}/3");

        UpdateEpiText();

        if (selectedEPIIndex >= totalEPI && !allEpiDone)
        {
            allEpiDone = true;

            Debug.Log("All EPIs have been selected!");

            if (epiCounterText != null)
                epiText.text = "ALL EPIs have been selected!";
                StartCoroutine(HideMessageAfterDelay());

            if (audioSource != null && allEpiSelectedClip != null)
                audioSource.PlayOneShot(allEpiSelectedClip);
        }
    }

    IEnumerator HideMessageAfterDelay() 
    { 
        yield return new WaitForSeconds(4f); 
        epiCounterText.gameObject.SetActive(false);
        epiText.gameObject.SetActive(false);
    }

    private void UpdateEpiText()
    {
        if (epiCounterText != null)
            epiCounterText.text = "EPI worn: "+ selectedEPIIndex+"/"+totalEPI;
    }
}
