using UnityEngine;

public class RegisterToManager : MonoBehaviour
{
    void Awake()
    {
        // On cherche le manager dans la scène et on lui donne "notre" référence
        GameManager manager = Object.FindFirstObjectByType<GameManager>();
        if (manager != null) manager.GetComponent<DechetsPooling>().poolTransform = gameObject;
    }
}