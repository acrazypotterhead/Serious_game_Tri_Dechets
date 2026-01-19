using UnityEngine;
using TMPro;
using System.Collections;


public class IsolementZoneManagement : MonoBehaviour
{

    public TMP_Text instructions;
    public float displayDuration = 4f;

     private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("NotAcid") && !other.CompareTag("LeakingWaste"))
        {
            Debug.Log("Wrong item isolated. ERROR");
            return;
        }

        // make object NOT re-grabbable
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null)
        {
            grab.enabled = false;
        }
         //  freeze physics so it stays in place
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

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
            AnomalyManager.Instance.hasAnomaly = true; 
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
