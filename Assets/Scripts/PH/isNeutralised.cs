using UnityEngine;
using TMPro;
using System.Collections;

public class IsNeutralised : MonoBehaviour
{
    public Neutralisation neutralisation; 
    public TMP_Text infoText;

    private Color orange = new Color(1f, 0.5f, 0f);
    private Coroutine hideRoutine;
    
    private void Awake()
    {
        if (infoText != null)
            infoText.gameObject.SetActive(false);
    }

    private void SetInfo(string msg, Color color)
    {
        if (infoText == null) return;

        infoText.text = msg;
        infoText.color = color;

        // Activer le texte
        infoText.gameObject.SetActive(true);

        // Reset le timer
        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(HideAfterSeconds(3f));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (infoText != null) infoText.gameObject.SetActive(false);
        hideRoutine = null;
    }

    void OnCollisionEnter(Collision collision)
    {


        bool thisIsBaseNeutralisation = gameObject.CompareTag("AcideNeutralisation");
        bool thisIsAcideNeutralisation = gameObject.CompareTag("BaseNeutralisation");

        if (!thisIsBaseNeutralisation && !thisIsAcideNeutralisation) return;

        bool otherIsAcid = collision.gameObject.CompareTag("AcideNeutralisation");
        bool otherIsBase = collision.gameObject.CompareTag("BaseNeutralisation");

        if (!otherIsAcid && !otherIsBase) return;

        // Succès : acide + base
        if ((thisIsBaseNeutralisation && otherIsBase) || (thisIsAcideNeutralisation && otherIsAcid))
        {
            gameObject.tag = "Neutre";
            collision.gameObject.tag = "Neutre";
            SetInfo("Neutralisation réussie : acide + base -> neutre.", Color.green);
        }
        else
        {
            if (thisIsBaseNeutralisation && otherIsAcid)
                SetInfo("Impossible : un acide ne neutralise pas un autre acide.", Color.red);
            else
                SetInfo("Impossible : une base ne neutralise pas une autre base.", Color.red);
        }
    }
}
