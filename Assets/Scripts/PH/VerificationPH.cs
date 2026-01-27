using UnityEngine;
using System.Collections;
using TMPro;

public class VerificationPH : MonoBehaviour
{
    public GameObject bandelette;

    [Header("Visual revert")]
    public float revertDelay = 5f;

    [Header("Info UI (optional)")]
    public TMP_Text infoText;
    public float infoDuration = 3f;

    [Header("Neutralisation thresholds")]
    private float acidNeedsNeutralisationBelow = 2f; // pH < 2 => AcideNeutralisation
    private float baseNeedsNeutralisationAbove = 12f; // pH > 12 => BaseNeutralisation

    private Renderer bandeletteRenderer;
    private Material originalMaterial;
    private Coroutine revertRoutine;
    private Coroutine infoRoutine;

    void Awake()
    {
        bandeletteRenderer = bandelette.GetComponent<Renderer>();
        originalMaterial = bandeletteRenderer.sharedMaterial;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("AcidePotentiel")) return;

        PotentialAcide acideComponent = collision.gameObject.GetComponent<PotentialAcide>();
        if (acideComponent == null) return;

        acideComponent.VerifiedAcide = true;

        float pH = acideComponent.categoryBandelette;

        // Couleur + tag selon pH
        Color targetColor;
        string newTag;

        if (pH < 7f)
        {
            targetColor = Color.red;

            // Acide à neutraliser ?
            if (pH < acidNeedsNeutralisationBelow)
            {
                newTag = "AcideNeutralisation";
                ShowInfo("Cet acide a besoin d’être neutralisé avant le tri !");
            }
            else
            {
                newTag = "Acide";
            }
        }
        else if (Mathf.Approximately(pH, 7f))
        {
            targetColor = Color.green;
            newTag = "Neutre";
        }
        else
        {
            targetColor = Color.blue;

            // Base à neutraliser ?
            if (pH > baseNeedsNeutralisationAbove)
            {
                newTag = "BaseNeutralisation";
                ShowInfo("Cette base a besoin d’être neutralisée avant le tri !");
            }
            else
            {
                newTag = "Basique";
            }
        }

        // Appliquer couleur temporaire
        bandeletteRenderer.material.color = targetColor;

        // Tag (attention: les tags doivent exister dans Unity > Tags & Layers)
        collision.gameObject.tag = newTag;

        Debug.Log($"pH vérifié: {collision.gameObject.name} | pH={pH} | tag={newTag}");

        // Reset timer de retour matériau d’origine
        if (revertRoutine != null) StopCoroutine(revertRoutine);
        revertRoutine = StartCoroutine(RevertAfterDelay(revertDelay));
    }

    private void ShowInfo(string message)
    {
        if (infoText == null) return;

        infoText.gameObject.SetActive(true);
        infoText.text = message;

        if (infoRoutine != null) StopCoroutine(infoRoutine);
        infoRoutine = StartCoroutine(HideInfoAfterDelay(infoDuration));
    }

    private IEnumerator HideInfoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        infoText.text = "";
        infoText.gameObject.SetActive(false);
        infoRoutine = null;
    }

    private IEnumerator RevertAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        bandeletteRenderer.sharedMaterial = originalMaterial;
        revertRoutine = null;
    }
}
