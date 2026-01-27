using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// Add this component to a GameObject and call the <see cref="IncrementText"/> method
    /// in response to a Unity Event to update a text display to count up with each event.
    /// </summary>
    public class IsolementButtonBehavior : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Text component this behavior uses to display the incremented value.")]
        public TMP_Text instructions; 
        public float displayDuration = 4f;
        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip validationClip;
        public AudioClip wrongClip;

        [Header("Dependencies")]
        public ScoreManager scoreManager;
        [Header("Haptics")]
        public HapticFeedback rightHaptic;   
        public HapticFeedback leftHaptic;   

        public DechetsPooling dechetsPooling;



        protected void Awake()
        {
            if (instructions == null)
                Debug.LogWarning("Missing required Text component reference. Use the Inspector window to assign which Text component to increment.", this);
        }

        public void MakeHappen()
        {
            instructions.gameObject.SetActive(true);

            // 1No anomaly at all
            if (!AnomalyManager.Instance.hasAnomaly)
            {
                instructions.text = "No Anomaly to report.";
                instructions.color = Color.white;
            }
            else
            {
                instructions.text = "Anomaly reported.";
                if (leftHaptic != null)
                    leftHaptic.Pulse(0.3f, 0.2f);
                if (rightHaptic != null)
                    rightHaptic.Pulse(0.3f, 0.2f);
                
                ScoreManager.Instance.RegisterSuccess();
                dechetsPooling.SpawnDechet();
                if (audioSource && validationClip)
                    audioSource.PlayOneShot(validationClip);

                instructions.color = Color.white;
                FindObjectOfType<IncrementText>().Increment();
                AnomalyManager.Instance.ResetAnomaly();
            }

            StartCoroutine(HideMessageAfterDelay());
        }



        IEnumerator HideMessageAfterDelay() 
        { 
            yield return new WaitForSeconds(displayDuration); 
            instructions.gameObject.SetActive(false); 
        }
    }
}
