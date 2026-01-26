using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[DisallowMultipleComponent]
public class LeakOnGrab : MonoBehaviour
{
    [Header("Leak Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float leakProbability = 0.3f;

    [SerializeField] private GameObject leakPuddlePrefab;
    [SerializeField] private Vector3 puddleOffset = new Vector3(0, -0.07f, 0);

    private XRGrabInteractable grab;
    private bool leakAlreadyChecked = false;
    private GameObject spawnedPuddle;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Only roll once
        if (leakAlreadyChecked) return;
        leakAlreadyChecked = true;

        // Random chance
        if (Random.value > leakProbability) return;

        StartLeak();
    }

    private void StartLeak()
    {
        // Change tag
        gameObject.tag = "LeakingWaste";

        // Register anomaly
        //AnomalyManager.Instance.hasAnomaly = true;
        AnomalyManager.Instance.leakContained = false;

        // Spawn puddle
        if (leakPuddlePrefab != null)
        {
            spawnedPuddle = Instantiate(
                leakPuddlePrefab,
                transform.position + puddleOffset,
                Quaternion.identity
            );
            Debug.Log("Puddle spawned.");
        }

    }
}
