using UnityEngine;
using TMPro;
using System.Collections;

public class SortingDechets : MonoBehaviour
{
    public TMP_Text feedbackText;
    public ScoreManager scoreManager;

    private Coroutine hideRoutine;
    [Header("Audio Feedback")]
    public AudioSource audioSource;
    public AudioClip successClip;
    public AudioClip errorClip;

    void OnCollisionEnter(Collision collision)
    {
        Color orange = new Color(1f, 0.5f, 0f); // RGB orange

        // Helper
        void SetFeedback(string msg, Color color)
        {
            feedbackText.text = msg;
            feedbackText.color = color;

            // Active le TMP
            feedbackText.gameObject.SetActive(true);

            // Reset le timer
            if (hideRoutine != null) StopCoroutine(hideRoutine);
            hideRoutine = StartCoroutine(HideAfterSeconds(3f));
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
                    if (audioSource && successClip)
                        audioSource.PlayOneShot(successClip);
                    Debug.Log("Correctly sorted waste: " + collision.gameObject.tag);
                    ScoreManager.Instance.RegisterSuccess();
                    Destroy(collision.gameObject);
                }
                else
                {
                    SetFeedback("Incorrectly sorted waste!", Color.red);
                    if (audioSource && errorClip)
                        audioSource.PlayOneShot(errorClip);
                    Debug.Log("Incorrectly sorted waste: " + collision.gameObject.tag);
                    ScoreManager.Instance.RegisterError("Incorrectly sorted waste.");
                }
            }
            else
            {
                SetFeedback("The potential acid has not been verified yet.", orange);
                if (audioSource && errorClip)
                    audioSource.PlayOneShot(errorClip);
                ScoreManager.Instance.RegisterError("Unverified acid sorted.");
                Debug.Log("The acid has not been verified yet: " + collision.gameObject.tag);
            }
        }
        else
        {
            if (collision.gameObject.tag.Contains(gameObject.name))
            {
                SetFeedback("Correctly sorted waste!", Color.green);
                if (audioSource && successClip)
                    audioSource.PlayOneShot(successClip);
                Debug.Log("Correctly sorted waste: " + collision.gameObject.tag);
                Destroy(collision.gameObject);
                ScoreManager.Instance.RegisterSuccess();
            }
            else
            {
                SetFeedback("Incorrectly sorted waste!", Color.red);
                if (audioSource && errorClip)
                    audioSource.PlayOneShot(errorClip);
                Debug.Log("Incorrectly sorted waste: " + collision.gameObject.tag);
                ScoreManager.Instance.RegisterError("Incorrectly sorted waste.");
            }
        }
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        feedbackText.gameObject.SetActive(false);
        hideRoutine = null;
    }
}
