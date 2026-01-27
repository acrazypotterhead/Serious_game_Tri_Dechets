using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;



public class HapticFeedback : MonoBehaviour
{
    public HapticImpulsePlayer hapticPlayer;

    public void Pulse(float amplitude, float duration)
    {
        if (hapticPlayer == null) return;

        hapticPlayer.SendHapticImpulse(amplitude, duration);
    }
}
