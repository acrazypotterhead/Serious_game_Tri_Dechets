using UnityEngine;
using TMPro;

public class EPIManager : MonoBehaviour
{
    public static EPIManager Instance { get; private set; }

    public int selectedEPIIndex = 0;
    public TMP_Text epiText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        selectedEPIIndex = 0;
    }


    public void RegisterEPISelected()
    {
        selectedEPIIndex++;
        Debug.Log($"EPI selected: {selectedEPIIndex}/3");

        if (selectedEPIIndex >= 3){
            Debug.Log("All EPIs have been selected!");
            epiText.text = "ALL EPIs have been selected!";
        }
    }
}
