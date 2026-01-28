using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    [Tooltip("Le nom commun sans le suffixe (ex: 'Lab', 'Socle', 'chemset')")]
    [SerializeField] private string baseName;

    void Start()
    {
        LoadAsset();
    }

    void LoadAsset()
    {
        // 1. Déterminer le suffixe selon la plateforme
        string suffix = "";

#if UNITY_ANDROID
        // Si on est sur le Quest (Android)
        suffix = "-MQ";
#else
        // Si on est sur PC (Editeur ou Build PCVR)
        suffix = "-PCVR";
#endif

        // 2. Construire le nom complet (ex: "Lab" + "-PCVR" = "Lab-PCVR")
        string finalAddress = baseName + suffix;

        Debug.Log($"Chargement de l'objet : {finalAddress} sur {this.gameObject.name}");

        // 3. Charger et instancier en tant qu'enfant de cet objet (l'Ancre)
        Addressables.InstantiateAsync(finalAddress, this.transform).Completed += OnAssetLoaded;
    }

    private void OnAssetLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            // L'objet est chargé. 
            // Comme on a utilisé 'this.transform' dans InstantiateAsync, 
            // il est déjà positionné et orienté comme l'ancre.

            // Optionnel : S'assurer que la position locale est bien à 0,0,0
            obj.Result.transform.localPosition = Vector3.zero;
            obj.Result.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError($"ERREUR CRITIQUE : Impossible de charger l'asset '{baseName}'. Vérifie le nom dans le groupe Addressables.");
        }
    }
}