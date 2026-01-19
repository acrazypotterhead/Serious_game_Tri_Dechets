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
                instructions.text = "Aucune anomalie à signaler";
                instructions.color = Color.white;
            }
            // Anomaly exists AND it is a leak AND not contained
            else if (!AnomalyManager.Instance.leakContained)
            {
                instructions.text = "Contenir la fuite avant signalement";
                instructions.color = new Color32(255, 165, 0, 255);
            }
            // Anomaly exists AND (not a leak OR leak contained)
            else
            {
                instructions.text = "Anomalie signalée";
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
