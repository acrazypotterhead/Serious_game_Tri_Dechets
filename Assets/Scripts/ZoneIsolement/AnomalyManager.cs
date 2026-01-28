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

         if (splashPenaltyRoutine == null)
            splashPenaltyRoutine = StartCoroutine(SplashSecurityPenalty());

  
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
        // 1️⃣ Attente avant pénalité
        yield return new WaitForSeconds(securityPenaltyInterval);

        // Si résolu pendant l’attente → on saute cette itération
        if (!splashActive || splashContained)
            continue;

        // 2️⃣ Pénalité
        ScoreManager.Instance.RegisterSecurityError(10);
        ScoreManager.Instance.RegisterError(ErrorType.AccidentNotTreated);

        // 3️⃣ Message
        instructions.gameObject.SetActive(true);
        instructions.text =
            "You have been splashed with a chemical.\nClean yourself immediately.";
        instructions.color = Color.red;

        // 4️⃣ Affichage 4s
        yield return new WaitForSeconds(4f);
        instructions.gameObject.SetActive(false);
    }

    splashPenaltyRoutine = null;
}


    private IEnumerator LeakSecurityPenalty()
    {
        while (leakActive && !leakContained)
        {
            yield return new WaitForSeconds(securityPenaltyInterval);

            if (!leakActive || leakContained)
                continue;

            ScoreManager.Instance.RegisterSecurityError(10);
            ScoreManager.Instance.RegisterError(ErrorType.AccidentNotTreated);

            instructions.gameObject.SetActive(true);
            instructions.text = "The leak is still active. Take care of it.";
            instructions.color = Color.red;

            yield return new WaitForSeconds(4f);
            instructions.gameObject.SetActive(false);
        }

        leakPenaltyRoutine = null;
    }




    public void ResetAnomaly()
    {
        hasAnomaly = false;
        leakContained = false;
        splashContained = false;

    }
}
