using UnityEngine;
using TMPro;
using System.Collections;

public class InteractionsBeforeEPis : MonoBehaviour
{
    public EPIManager epiManager;
    public TMP_Text infoText;

    private Coroutine hideRoutine;

    public void Interacting()
    {
        // Si EPIs pas tous équipés
        if (epiManager.selectedEPIIndex < 3)
        {
            infoText.text = "EPIs are not equipped, you need to equip all EPIs!";

            // Annule un timer précédent si on ré-interagit
            if (hideRoutine != null) StopCoroutine(hideRoutine);
            hideRoutine = StartCoroutine(HideMessageAfterSeconds(3f));
        }
    }

    private IEnumerator HideMessageAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        infoText.text = "";
        hideRoutine = null;
    }

    public void NotHolding()
    {
        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = null;
        infoText.text = "";
    }
}
