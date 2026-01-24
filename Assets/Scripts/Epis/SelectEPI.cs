using UnityEngine;

public class SelectEPI : MonoBehaviour
{
    private bool _selected = false;

    public void EPIselected()
    {
        if (_selected) return;
        _selected = true;

        if (EPIManager.Instance != null)
            EPIManager.Instance.RegisterEPISelected();
        
        Debug.Log($"EPI {gameObject.name} selected.");

        gameObject.SetActive(false);
    }
}
