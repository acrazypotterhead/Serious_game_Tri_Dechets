using UnityEngine;

public class LabRoomTrigger : MonoBehaviour
{
    private bool hasCrossedDoor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        hasCrossedDoor = !hasCrossedDoor;

        if (hasCrossedDoor)
        {
            Debug.Log("Player ENTERED lab");

            // Start exposure ONLY if EPI not fully worn
            if (EPIManager.Instance.selectedEPIIndex < EPIManager.Instance.totalEPI)
                LabExposureController.Instance.StartExposure();
        }
        else
        {
            LabExposureController.Instance.PauseExposure();

        }
    }
}
