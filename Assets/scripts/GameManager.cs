using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    public BilanManager bilanManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Etat du jeu :" + newState);

        if (newState == GameState.Bilan)
        {
            bilanManager.ShowResults();
        }
    }

    

    void Start()
    {
        ChangeState(GameState.Incident);
    }
}
