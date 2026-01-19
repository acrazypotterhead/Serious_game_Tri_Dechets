using UnityEngine;
using TMPro;
using System.Collections;

public class IsolementZoneManagement : MonoBehaviour
{

    public TMP_Text instructions;
    public float displayDuration = 4f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NotAcid")
        {
            instructions.gameObject.SetActive(true);
            instructions.text = "Déchet Isolé.";
            instructions.color = new Color32(120, 61, 34, 255);
        }

        if(collision.gameObject.tag == "LeakingWaste")
        {
            instructions.gameObject.SetActive(true);
            instructions.text = "Bidon Isolé.";
             instructions.color = new Color32(120, 61, 34, 255);
        }
        AnomalyManager.Instance.hasAnomaly = true;
        StartCoroutine(HideMessageAfterDelay());
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        instructions.gameObject.SetActive(false);
    }
}
