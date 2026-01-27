using UnityEngine;

public class PotentialAcide : MonoBehaviour
{
    public bool VerifiedAcide;
    public float categoryBandelette; // random entre 0 et 7

    void OnDisable()
    {
        ResetAcide();
    }

    void Onenable()
    {
        ResetAcide();
    }

    void Start()
    {
        ResetAcide();
    }

    public void ResetAcide()
    {
        VerifiedAcide = false;
        categoryBandelette = Random.Range(0f, 13f); // 0.0 inclus, 7.0 exclus
        gameObject.tag = "AcidePotentiel";
    }
}
