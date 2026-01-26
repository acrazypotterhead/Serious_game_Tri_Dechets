using UnityEngine;
using TMPro;
using System.Collections;

public class AbsorbantBehavior : MonoBehaviour
{

    public TMP_Text instructions;
    public float displayDuration = 4f;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("LeakPuddle")) return;

        // Leak is treated
        AnomalyManager.Instance.leakContained = true;
        AnomalyManager.Instance.hasAnomaly = true;

        other.gameObject.SetActive(false);
        instructions.gameObject.SetActive(true);
        instructions.text = "Situation résolue.\nSignalez l'anomalie.";
        instructions.color = new Color32(255, 255, 255, 255);

        StartCoroutine(HideMessageAfterDelay());
    }

     IEnumerator HideMessageAfterDelay() 
        { 
            yield return new WaitForSeconds(displayDuration); 
            instructions.gameObject.SetActive(false); 
        }
}
