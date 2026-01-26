using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PictogramUI : MonoBehaviour
{
    public GameObject pictogramCanvas;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Start()
    {
        pictogramCanvas.SetActive(false);
    }


    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        pictogramCanvas.SetActive(true);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        pictogramCanvas.SetActive(false);
    }
}
