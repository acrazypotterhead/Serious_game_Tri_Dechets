using UnityEngine;
using TMPro;
using System.Collections;


public class IsolementZoneManagement : MonoBehaviour
{

    public TMP_Text instructions;
    public float displayDuration = 4f;
    [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip validationClip;
        public AudioClip wrongClip;
    [Header("Dependencies")]
        public ScoreManager scoreManager;

    void Start()
    {
        instructions.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
            return;

        if (!other.CompareTag("NotAcid") && !other.CompareTag("LeakingWaste"))
        {
            scoreManager.RegisterError("Wrong Item Isolated.");
            Destroy(other.gameObject);
            if (audioSource && wrongClip)
                audioSource.PlayOneShot(wrongClip);
            instructions.gameObject.SetActive(true);
            instructions.text = "Wrong Item Isolated.";
            instructions.color = new Color32(255, 165, 0, 255);
            StartCoroutine(HideMessageAfterDelay());
            return;
        }

        if (other.gameObject.tag == "NotAcid")
        {
            instructions.gameObject.SetActive(true);
            if (audioSource && validationClip)
                    audioSource.PlayOneShot(validationClip);
            instructions.text = "Isolated Waste. Report the anomaly.";
            instructions.color = new Color32(255, 255, 255, 255);
            AnomalyManager.Instance.hasAnomaly = true;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "LeakingWaste")
        {
            
            instructions.gameObject.SetActive(true);
            if (audioSource && validationClip)
                    audioSource.PlayOneShot(validationClip);
            instructions.text = "Isolated Can.\nReport the anomaly.";
            instructions.color = new Color32(255, 255, 255, 255);
            AnomalyManager.Instance.hasAnomaly = true; 
            AnomalyManager.Instance.leakContained = false;
            Destroy(other.gameObject);
        }
        StartCoroutine(HideMessageAfterDelay());
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        instructions.gameObject.SetActive(false);
    }
}
