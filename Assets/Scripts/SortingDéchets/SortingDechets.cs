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

    public DechetsPooling dechetsPooling;
   
   
   
    void OnCollisionEnter(Collision collision)
    {
        Color orange = new Color(1f, 0.5f, 0f); // RGB orange


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
                    //Debug.Log("Correctly sorted waste: " + collision.gameObject.tag);
                    ScoreManager.Instance.RegisterSuccess();
                    dechetsPooling.DespawnDechet(collision.gameObject);
                    dechetsPooling.SpawnDechet();
                }
                else
                {
                    SetFeedback("Incorrectly sorted waste!", Color.red);
                    if (audioSource && errorClip)
                        audioSource.PlayOneShot(errorClip);
                    Debug.Log("Incorrectly sorted waste: " + collision.gameObject.tag);
                    ScoreManager.Instance.RegisterError(ErrorType.WrongSorting);
                }
            }
            else
            {
                SetFeedback("The potential acid has not been verified yet.", orange);
                if (audioSource && errorClip)
                    audioSource.PlayOneShot(errorClip);
                ScoreManager.Instance.RegisterError(ErrorType.UnverifiedAcid);
                ScoreManager.Instance.RegisterEnvironmentError(10);
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
                //Destroy(collision.gameObject);
                dechetsPooling.DespawnDechet(collision.gameObject);
                dechetsPooling.SpawnDechet();
                ScoreManager.Instance.RegisterSuccess();
            }
            else
            {
                SetFeedback("Incorrectly sorted waste!", Color.red);
                if (audioSource && errorClip)
                    audioSource.PlayOneShot(errorClip);
                Debug.Log("Incorrectly sorted waste: " + collision.gameObject.tag);
                ScoreManager.Instance.RegisterError(ErrorType.WrongSorting);
            }
        }
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        feedbackText.gameObject.SetActive(false);
        hideRoutine = null;
    }

    
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
}
