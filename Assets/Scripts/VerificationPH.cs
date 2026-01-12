using UnityEngine;

public class VerificationPH : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "AcidePotentiel")
        {
            PotentialAcide acideComponent = collision.gameObject.GetComponent<PotentialAcide>();
            acideComponent.VerifiedAcide = true;
            Debug.Log("Acid verified for pH: " + collision.gameObject.name + " with bandelette category: " + acideComponent.catgegorybandelette);
        }
    }
}
