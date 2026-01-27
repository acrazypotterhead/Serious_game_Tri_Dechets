using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[DisallowMultipleComponent]
public class SplashOnGrab : MonoBehaviour
{
    [Header("Splash Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float splashProbability = 0.3f;

    [SerializeField] private GameObject splashEffectPrefab;
    [SerializeField] private Vector3 localOffset = Vector3.zero;
    public bool splashed = false;

    private XRGrabInteractable grab;

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
        if (Random.value > splashProbability) return;
        StartSplash();
    }

    private void StartSplash()
    {
        splashed = true;

        gameObject.tag = "SplashingWaste";

        AnomalyManager.Instance.splashActive = true;
        if (splashEffectPrefab == null) return;

        // ✅ instancie en enfant de l'objet grab
        GameObject splash = Instantiate(splashEffectPrefab, transform);
        splash.transform.localPosition = localOffset;
        splash.transform.localRotation = Quaternion.identity;
    }
}
