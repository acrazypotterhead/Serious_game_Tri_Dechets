using UnityEngine;
using UnityEngine.UI;

public class ActivateShower : MonoBehaviour
{
    public GameObject rainWater;
    public bool isShowering = false;
    public HasBeenSplashed hasBeenSplashed;
    public Slider splashSlider;

    void Start()
    {

            rainWater.SetActive(false);

    }

   void OnTriggerEnter(Collider other)
   {

        rainWater.SetActive(true);
        if (hasBeenSplashed != null)
        {
            AnomalyManager.Instance.ContainLeak();
            hasBeenSplashed.ResetExposure();
            hasBeenSplashed.continueExposure = false;
            splashSlider.gameObject.SetActive(false);

        }
   }

    

}
