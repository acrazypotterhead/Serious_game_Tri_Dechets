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

    [Header("Leak Penalty Settings")]
    public float securityPenaltyInterval = 20f;

    private Coroutine leakPenaltyRoutine;

     public bool splashActive = false;

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

            Debug.Log("Security penalty applied: leak not handled");
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
        splashActive = false;
        leakActive = false;

        if (leakPenaltyRoutine != null)
        {
            StopCoroutine(leakPenaltyRoutine);
            leakPenaltyRoutine = null;
        }
    }
}
