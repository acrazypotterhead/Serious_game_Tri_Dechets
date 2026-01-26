using UnityEngine;
using TMPro;

public class SortingDechets : MonoBehaviour
{
    public TMP_Text feedbackText;
    public ScoreManager scoreManager;
    

    void OnCollisionEnter(Collision collision)
    {
        Color orange = new Color(1f, 0.5f, 0f); // RGB orange
        // Petit helper pour éviter de répéter
        void SetFeedback(string msg, Color color)
        {
            feedbackText.text = msg;
            feedbackText.color = color;
        }

        if (collision.gameObject.CompareTag("AcidePotentiel"))
        {
            PotentialAcide acideComponent = collision.gameObject.GetComponent<PotentialAcide>();
            if (acideComponent == null) return;

            if (acideComponent.VerifiedAcide)
            {
                Debug.Log("The acid has been verified: " + collision.gameObject.name);

                if (collision.gameObject.tag.Contains(gameObject.name))
                {
                    SetFeedback("Correctly sorted waste!", Color.green);
                    Debug.Log("Correctly sorted waste: " + collision.gameObject.tag);
                    ScoreManager.Instance.RegisterSuccess();
                    Destroy(collision.gameObject);
                }
                else
                {
                    SetFeedback("Incorrectly sorted waste!", Color.red);
                    Debug.Log("Incorrectly sorted waste: " + collision.gameObject.tag);
                    ScoreManager.Instance.RegisterError("Incorrectly sorted waste.");
                }
            }
            else
            {
                // Informatif (jaune)
                SetFeedback("The potential acid has not been verified yet.", orange);
                ScoreManager.Instance.RegisterError("Unverified acid sorted.");

                Debug.Log("The acid has not been verified yet: " + collision.gameObject.tag);
            }
        }
        else
        {
            if (collision.gameObject.tag.Contains(gameObject.name))
            {
                SetFeedback("Correctly sorted waste!", Color.green);
                Debug.Log("Correctly sorted waste: " + collision.gameObject.tag);
                Destroy(collision.gameObject);
                ScoreManager.Instance.RegisterSuccess();
            }
            else
            {
                SetFeedback("Incorrectly sorted waste!", Color.red);
                Debug.Log("Incorrectly sorted waste: " + collision.gameObject.tag);
                ScoreManager.Instance.RegisterError("Incorrectly sorted waste.");
            }
        }
    }
}
