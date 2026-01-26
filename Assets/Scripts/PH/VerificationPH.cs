using UnityEngine;
using System.Collections;

public class VerificationPH : MonoBehaviour
{
    public GameObject bandelette;
    public float revertDelay = 5f;

    private Renderer bandeletteRenderer;
    private Material originalMaterial;
    private Coroutine revertRoutine;

    void Awake()
    {
        bandeletteRenderer = bandelette.GetComponent<Renderer>();
        // Important: sharedMaterial pour garder une référence au matériau d'origine (asset)
        originalMaterial = bandeletteRenderer.sharedMaterial;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("AcidePotentiel")) return;

        PotentialAcide acideComponent = collision.gameObject.GetComponent<PotentialAcide>();
        if (acideComponent == null) return;

        acideComponent.VerifiedAcide = true;

        // Choix couleur selon catégorie
        Color targetColor;
        string newTag;

        if (acideComponent.categoryBandelette < 7f)
        {
            targetColor = Color.red;
            newTag = "Acide";
        }
        else if (acideComponent.categoryBandelette == 7f)
        {
            targetColor = Color.green;
            newTag = "Neutre";
        }
        else
        {
            targetColor = Color.blue;
            newTag = "Basique";
        }

        // Appliquer couleur (material => instance, ne modifie pas l'asset)
        bandeletteRenderer.material.color = targetColor;
        collision.gameObject.tag = newTag;

        Debug.Log("Acid verified for pH: " + collision.gameObject.name +
                  " with bandelette category: " + acideComponent.categoryBandelette);

        // Reset le timer de retour
        if (revertRoutine != null) StopCoroutine(revertRoutine);
        revertRoutine = StartCoroutine(RevertAfterDelay(revertDelay));
    }

    private IEnumerator RevertAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Remet le matériau d’origine
        bandeletteRenderer.sharedMaterial = originalMaterial;

        revertRoutine = null;
    }
}
