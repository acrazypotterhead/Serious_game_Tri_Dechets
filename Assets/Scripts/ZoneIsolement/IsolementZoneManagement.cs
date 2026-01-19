using UnityEngine;
using TMPro;
using System.Collections;

public class IsolementZoneManagement : MonoBehaviour
{

    public TMP_Text instructions;
    public float displayDuration = 4f;

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NotAcid")
        {
            instructions.gameObject.SetActive(true);
            instructions.text = "Déchet Isolé.\nSignalez l'anomalie.";
            instructions.color = new Color32(255, 255, 255, 255);
            AnomalyManager.Instance.hasAnomaly = true;
        }

        if(other.gameObject.tag == "LeakingWaste")
        {
            
            instructions.gameObject.SetActive(true);
            instructions.text = "Bidon Isolé.\nSignalez l'anomalie.";
            instructions.color = new Color32(255, 255, 255, 255);
            AnomalyManager.Instance.leakContained = false;
        }
        StartCoroutine(HideMessageAfterDelay());
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        instructions.gameObject.SetActive(false);
    }
}
