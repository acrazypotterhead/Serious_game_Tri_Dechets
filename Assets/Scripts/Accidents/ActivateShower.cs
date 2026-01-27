using UnityEngine;
using UnityEngine.UI;

public class ActivateShower : MonoBehaviour
{
    public GameObject rainWater;
    public bool isShowering = false;
    public HasBeenSplashed hasBeenSplashed;
    public Slider splashSlider;
    public AudioSource audioSource;

    void Start()
    {

            rainWater.SetActive(false);

    }

    void OnTriggerEnter(Collider other)
    {

            rainWater.SetActive(true);
            if (hasBeenSplashed != null)
            {
                hasBeenSplashed.ResetExposure();
                hasBeenSplashed.continueExposure = false;
                splashSlider.gameObject.SetActive(false);

            }
            audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (audioSource != null) audioSource.Stop();
        if (rainWater != null) rainWater.SetActive(false);
    }

    

}
