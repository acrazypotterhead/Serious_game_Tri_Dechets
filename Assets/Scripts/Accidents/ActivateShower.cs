using UnityEngine;
using UnityEngine.UI;

public class ActivateShower : MonoBehaviour
{
    public GameObject rainWater;
    public bool isShowering = false;
    public HasBeenSplashed hasBeenSplashed;
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
            AnomalyManager.Instance.ContainSplash();

        }
        audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (audioSource != null) audioSource.Stop();
        if (rainWater != null) rainWater.SetActive(false);
    }

    

}
