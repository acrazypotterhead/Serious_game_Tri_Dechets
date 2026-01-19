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
            if (acideComponent.catgegorybandelette == "red")
            {
                bandelette.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                bandelette.GetComponent<Renderer>().material.color = Color.green;
            }
            Debug.Log("Acid verified for pH: " + collision.gameObject.name + " with bandelette category: " + acideComponent.catgegorybandelette);
        }

    }
}
