using UnityEngine;
using TMPro;
using System.Collections;

public class IsolementButtonBehavior : MonoBehaviour
{
    public TMP_Text instructions;
    public float displayDuration = 4f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        instructions.gameObject.SetActive(true);

        if (AnomalyManager.Instance.hasAnomaly)
        {
            instructions.text = "Anomalie signalée";
            instructions.color = new Color32(120, 61, 34, 255);
            AnomalyManager.Instance.hasAnomaly = false;
        }
        else
        {
            instructions.text = "Aucune anomalie à signaler";
            instructions.color = new Color32(180, 180, 180, 255);

        }

        StartCoroutine(HideMessageAfterDelay());
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        instructions.gameObject.SetActive(false);
    }
}
