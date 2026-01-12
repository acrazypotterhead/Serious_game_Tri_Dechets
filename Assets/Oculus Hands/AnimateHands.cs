using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAction;
    public InputActionProperty gripAction;
    public Animator handAnimator;

    void Update()
    {
        float triggerValue = pinchAction.action.ReadValue<float>();


        float gripValue = gripAction.action.ReadValue<float>();



    }
}
