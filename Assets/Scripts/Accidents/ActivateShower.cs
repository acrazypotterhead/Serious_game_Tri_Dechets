using UnityEngine;

public class ActivateShower : MonoBehaviour
{
    public GameObject rainWater;
    public bool isShowering = false;

    void Start()
    {

            rainWater.SetActive(false);

    }

   void OnTriggerEnter(Collider other)
   {

        rainWater.SetActive(true);
       
   }

    

}
