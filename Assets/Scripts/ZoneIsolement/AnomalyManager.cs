using UnityEngine;
using System.Collections;
using TMPro;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance;

    public bool hasAnomaly = false;
    public TMP_Text instructions;

    [Header("Leak State")]
    public bool leakActive = false;
    // this checks if the user has contained the leaking from the leaking container
     public bool leakContained = false;

    [Header("Splash State")]
    public bool splashActive = false;
     public bool splashContained = false;

    [Header("Penalty Settings")]
    public float securityPenaltyInterval = 20f;

    private Coroutine leakPenaltyRoutine;
    private Coroutine splashPenaltyRoutine;


    private void Awake()
    {
        Instance = this;
    }

    public void StartLeak()
    {
        if (leakActive) return;

        leakActive = true;
        leakContained = false;

        if (leakPenaltyRoutine == null)
            leakPenaltyRoutine = StartCoroutine(LeakSecurityPenalty());
    }

    public void StartSplash()
    {

        if (splashActive) return;

        splashActive = true;
        splashContained = false;

         if (leakPenaltyRoutine == null)
            leakPenaltyRoutine = StartCoroutine(SplashSecurityPenalty());

  
    }

    // usage of absorbant
    public void ContainLeak()
    {
        if (!leakActive) return;

        leakContained = true;
        leakActive = false;

        if (leakPenaltyRoutine != null)
        {
            StopCoroutine(leakPenaltyRoutine);
            leakPenaltyRoutine = null;
        }

        Debug.Log("Leak contained");
    }

    public void ContainSplash()
    {
        if (!splashActive) return;

        splashContained = true;
        splashActive = false;

        if (splashPenaltyRoutine != null)
        {
            StopCoroutine(splashPenaltyRoutine);
            splashPenaltyRoutine = null;
        }

    }

    private IEnumerator SplashSecurityPenalty()
    {
        while (splashActive && !splashContained)
        {
            yield return new WaitForSeconds(securityPenaltyInterval);

            instructions.gameObject.SetActive(true);
            instructions.text = "You still haven't showered. Take care of it, it could be toxic.";
            instructions.color = Color.red;

            StartCoroutine(HideMessageAfterDelay());
            ScoreManager.Instance.RegisterSecurityError(10);
            ScoreManager.Instance.RegisterError(ErrorType.AccidentNotTreated);

        }

        splashPenaltyRoutine = null;
    }

    private IEnumerator LeakSecurityPenalty()
    {
        while (leakActive && !leakContained)
        {
            yield return new WaitForSeconds(securityPenaltyInterval);

            instructions.gameObject.SetActive(true);
            instructions.text = "The leak is still active. Take care of it.";
            instructions.color = Color.red;

            StartCoroutine(HideMessageAfterDelay());
            ScoreManager.Instance.RegisterSecurityError(10);
            ScoreManager.Instance.RegisterError(ErrorType.AccidentNotTreated);

        }

        leakPenaltyRoutine = null;
    }
     IEnumerator HideMessageAfterDelay() 
        { 
            yield return new WaitForSeconds(3f); 
            instructions.gameObject.SetActive(false); 
        }

    public void ResetAnomaly()
    {
        hasAnomaly = false;
        leakContained = false;
        splashContained = false;
        splashActive = false;
        leakActive = false;

        if (leakPenaltyRoutine != null)
        {
            StopCoroutine(leakPenaltyRoutine);
            leakPenaltyRoutine = null;
        }

        if (splashPenaltyRoutine != null)
        {
            StopCoroutine(splashPenaltyRoutine);
            splashPenaltyRoutine = null;
        }
    }
}
