using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class Neutralisation : MonoBehaviour
{
    public bool OnZone;
    public TMP_Text infoText;
    public ScoreManager scoreManager;

    private Coroutine hideRoutine;

    public GameObject Socket;

    private void Awake()
    {
        if (infoText != null)
            infoText.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        // TODO / METTRE UN SOCKET ?
        
        // On récupère le bon component sur l'objet qui entre
        PotentialAcide pa = collision.gameObject.GetComponent<PotentialAcide>();

        if (pa != null && pa.VerifiedAcide)
        {
            OnZone = true;
        }
        else
        {
            if(pa.tag != "Acide2Neutre" && pa.tag != "Base2Neutre"){
                ShowInfo("L'élement n'a pas besoin d'être neutralisé.", Color.green);
            }
            else {
                    OnZone = false;
                    ShowInfo("The acid or the bas, needs to be verified with a PH test before being neutralized.", Color.red);
                    ScoreManager.Instance.RegisterError(ErrorType.Neutralize);
                    ScoreManager.Instance.RegisterEnvironmentError(10);
            }

        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Acide2Neutre") ||
            collision.gameObject.CompareTag("Base2Neutre"))
        {
            OnZone = false;
        }
    }

    private void ShowInfo(string msg, Color color)
    {
        if (infoText == null) return;

        infoText.text = msg;
        infoText.color = color;
        infoText.gameObject.SetActive(true);

        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(HideAfterSeconds(3f));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        infoText.gameObject.SetActive(false);
        hideRoutine = null;
    }


}
