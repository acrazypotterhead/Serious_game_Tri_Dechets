using UnityEngine;

public class VerificationPH : MonoBehaviour
{
    public GameObject bandelette;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "AcidePotentiel")
        {
            PotentialAcide acideComponent = collision.gameObject.GetComponent<PotentialAcide>();
            acideComponent.VerifiedAcide = true;
            // si c'est un acide on affiche la bandelette en couleur rouge
            bandelette.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("Acid verified for pH: " + collision.gameObject.name + " with bandelette category: " + acideComponent.catgegorybandelette);
        }
        else
        {
            bandelette.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
