using UnityEngine;

public class EPIManager : MonoBehaviour
{
    public static EPIManager Instance { get; private set; }

    private int selectedEPIIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        selectedEPIIndex = 0;
    }

    public void Update()
    {
        Debug.Log($"Current selected EPI index: {selectedEPIIndex}");
    }

    public void RegisterEPISelected()
    {
        selectedEPIIndex++;
        Debug.Log($"EPI selected: {selectedEPIIndex}/3");

        if (selectedEPIIndex >= 3)
            Debug.Log("All EPIs have been selected!");
    }
}
