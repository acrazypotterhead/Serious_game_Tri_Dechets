using UnityEngine;
using UnityEngine.UI;

public class HasBeenSplashed : MonoBehaviour
{
    private SplashOnGrab splashOnGrab;
    public Slider splashSlider;

    [Header("Exposure")]
    public float exposureLevel = 0f;
    public float exposureIncreasePerSecond = 5f;
    public float maxExposure = 100f;
    public bool continueExposure = false;
    private SplashOnGrab[] splashScripts;

    void Start()
    {
        splashSlider.gameObject.SetActive(false);

        if (splashSlider != null)
        {
            splashSlider.minValue = 0f;
            splashSlider.maxValue = maxExposure;   
            splashSlider.value = exposureLevel;
        }
    }


    void Update()
    {
        splashScripts = FindObjectsOfType<SplashOnGrab>();

        if (splashScripts != null && splashScripts.Length > 0 && splashScripts[0] != null){
            Debug.Log("Checking splash status...");

            if (splashScripts[0].splashed)
            {
                continueExposure = true;
            }
        }
        if (continueExposure)
        {
            StartingSplashExposure();
            splashSlider.gameObject.SetActive(true);
            
        }
    }
        

    private void StartingSplashExposure()
    {
        exposureLevel += exposureIncreasePerSecond * Time.deltaTime;
        exposureLevel = Mathf.Clamp(exposureLevel, 0f, maxExposure);

        if (splashSlider != null)
            splashSlider.value = exposureLevel;

        // Optionnel : quand c'est max
        if (Mathf.Approximately(exposureLevel, maxExposure))
        {
            Debug.Log("Exposure max !");

        }
    }

    // Optionnel : reset si douche / nettoyage
    public void ResetExposure()
    {
        exposureLevel = 0f;
        if (splashSlider != null)
            splashSlider.value = exposureLevel;

        if (splashScripts != null && splashScripts.Length > 0 && splashScripts[0] != null)
            splashScripts[0].splashed = false; // si tu veux reset le status
    }

}
