using UnityEngine;

public class WasteSorting : MonoBehaviour
{
    private Bin bin;

    private void Awake()
    {
        bin = GetComponent<Bin>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Waste waste = other.GetComponent<Waste>();
        if (waste == null ) return;

        if (waste.wasteType == WasteType.Acide && !waste.isNeutralized)
        {
            ScoreManager.Instance.RegisterError("Acide non neutralisé");
            return;
        }
        if (waste.wasteType == bin.acceptedType)
        {
            ScoreManager.Instance.RegisterSuccess();
            Destroy(other.gameObject);
        }
        else
        {
            ScoreManager.Instance.RegisterError("Mauvais tri");
            IncidentManager.Instance.TriggerIncident();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
