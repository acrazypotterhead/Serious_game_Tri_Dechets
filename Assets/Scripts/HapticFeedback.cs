using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    [Tooltip("GameObject that has the HapticImpulsePlayer component")]
    public GameObject hapticObject;

    public void Pulse(float amplitude, float duration)
    {
        if (hapticObject == null) return;

        // Call HapticImpulsePlayer.SendHapticImpulse(float, float)
        hapticObject.SendMessage(
            "SendHapticImpulse",
            new float[] { amplitude, duration },
            SendMessageOptions.DontRequireReceiver
        );
    }
}
