using UnityEngine;
using TMPro;

public class InteractionsBeforeEPis : MonoBehaviour
{
    public EPIManager epiManager;
    public TMP_Text infoText;
    
    public void Interacting()
    {
        
        // if im interacting with something with the tag "Solvant, "AcidePotentiel" , DéchetCritique, Basique, Acide" then display : Epis are not equipped
        if (epiManager.selectedEPIIndex < 3)
        {
            infoText.text = "EPIs are not equipped, you need to equip all EPIs!";
        }
    }

    public void NotHolding()
    {
        infoText.text = "";
    }
}
