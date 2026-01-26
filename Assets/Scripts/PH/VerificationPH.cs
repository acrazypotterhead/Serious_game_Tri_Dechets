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
            if (acideComponent.categoryBandelette <7f)
            {
                bandelette.GetComponent<Renderer>().material.color = Color.red;
                collision.gameObject.tag = "Acide";
            }
            else if(acideComponent.categoryBandelette ==7f)
            {
                bandelette.GetComponent<Renderer>().material.color = Color.green;
                collision.gameObject.tag = "Neutre";
            }
            else if (acideComponent.categoryBandelette >7f)
            {
                bandelette.GetComponent<Renderer>().material.color = Color.blue;
                collision.gameObject.tag = "Basique";
            }
            Debug.Log("Acid verified for pH: " + collision.gameObject.name + " with bandelette category: " + acideComponent.categoryBandelette);
        }

    }
}
