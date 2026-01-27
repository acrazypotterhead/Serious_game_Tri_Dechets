using UnityEngine;
using UnityEngine.UI;

public class HasBeenSplashed : MonoBehaviour
{
    private SplashOnGrab splashOnGrab;
    [Header("Exposure")]
    private SplashOnGrab[] splashScripts;
    public AudioSource audioSource;


    void Update()
    {
        splashScripts = FindObjectsOfType<SplashOnGrab>();

        if (splashScripts != null && splashScripts.Length > 0 && splashScripts[0] != null){
            return;
        }
        else {
            audioSource.Stop();
        }

    }

}
