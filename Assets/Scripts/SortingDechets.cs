using UnityEngine;

public class SortingDechets : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "AcidePotentiel"){
            PotentialAcide acideComponent = collision.gameObject.GetComponent<PotentialAcide>();
        
            if (acideComponent.VerifiedAcide)
            {
                Debug.Log("The acid has been verified: " + collision.gameObject.name);
                if (collision.gameObject.tag.Contains(gameObject.name))
                {
                    Debug.Log("Correctly sorted waste: " + collision.gameObject.tag);
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("Incorrectly sorted waste: " + collision.gameObject.tag);
                }
            }
            else {
                Debug.Log("The acid has not been verified yet: " + collision.gameObject.tag);
            }
        }
        else {
            if (collision.gameObject.tag.Contains(gameObject.name))
                {
                    Debug.Log("Correctly sorted waste: " + collision.gameObject.tag);
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("Incorrectly sorted waste: " + collision.gameObject.tag);
                }
        }
    }
}

