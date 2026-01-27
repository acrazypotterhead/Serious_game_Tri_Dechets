using UnityEngine;
using TMPro;
using System.Collections;

public class AbsorbantBehavior : MonoBehaviour
{

    public TMP_Text instructions;
    public float displayDuration = 4f;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip spongerubClip;
    [Header("Haptics")]
    public HapticFeedback rightHaptic;   
    public HapticFeedback leftHaptic;   

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("LeakPuddle")) return;

        // Leak is treated
        if (audioSource && spongerubClip)
            audioSource.PlayOneShot(spongerubClip);
        if (leftHaptic != null)
            leftHaptic.Pulse(0.6f, 0.3f);
        if (rightHaptic != null)
            rightHaptic.Pulse(0.6f, 0.3f);
            
        AnomalyManager.Instance.leakContained = true;
        AnomalyManager.Instance.hasAnomaly = true;

        other.gameObject.SetActive(false);
        instructions.gameObject.SetActive(true);
        instructions.text = "Leak cleaned. If you haven't yet, report the anomaly.";
        instructions.color = new Color32(255, 255, 255, 255);

        StartCoroutine(HideMessageAfterDelay());
    }

     IEnumerator HideMessageAfterDelay() 
        { 
            yield return new WaitForSeconds(displayDuration); 
            instructions.gameObject.SetActive(false); 
        }
}
