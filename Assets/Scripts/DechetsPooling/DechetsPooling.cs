using UnityEngine;

public class DechetsPooling : MonoBehaviour
{
    public GameObject pool;
    public GameObject poolTransform;


    public void SpawnDechet()
    {
        // choose a random child from the pool, remove it from the pool, activate it and put it as the child of the poolTransform
        int randomIndex = Random.Range(0, pool.transform.childCount);
        GameObject dechet = pool.transform.GetChild(randomIndex).gameObject;
        dechet.SetActive(true);
        dechet.transform.SetParent(poolTransform.transform);
        dechet.transform.localPosition = Vector3.zero;
    }

    public void DespawnDechet(GameObject dechet)
    {
        dechet.SetActive(false);
        // move the dechet back to the pool as a child
        dechet.transform.SetParent(pool.transform);
    }

}
