using UnityEngine;
using TMPro;
using System.Collections;

public class EPIManager : MonoBehaviour
{
    public static EPIManager Instance { get; private set; }

    public int selectedEPIIndex = 0;
    public int totalEPI = 3;
    public TMP_Text epiCounterText;

    public DechetsPooling dechetsPooling;


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
            LabExposureController.Instance.ResetExposure();
            allEpiDone = true;
            epiCounterText.gameObject.SetActive(false);
            dechetsPooling.SpawnDechet();
        
        }
    }

    private void UpdateEpiText()
    {
        if (epiCounterText != null)
            epiCounterText.text = "EPI worn: "+ selectedEPIIndex+"/"+totalEPI;
    }
}
