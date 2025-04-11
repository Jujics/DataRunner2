using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance{get; private set;}
    [SerializeField]
    private GameObject player;
    private PlayerStateManager playerStateManager;
    [SerializeField]
    private GameObject menuCanvas;
    [SerializeField]
    private GameObject gameCanvas;
    [SerializeField]
    private CinematicManager cinematicManager;
    [SerializeField]
    private FirstQuestManager firstQuestManager;

    public enum GameState 
    {
        Menu,
        Cinematic,
        InGame,
        PostGame,
        EndGame
    }

    public GameState CurrentState;

    public void SwitchState(GameState newState)  
    {
        CurrentState = newState;
        Debug.Log($"Game state changed to: {newState}"); 
        HandleState();
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void HandleState()
    {
        switch(CurrentState)
        {
            case GameState.Menu:
                playerStateManager = player.GetComponent<PlayerStateManager>();
                playerStateManager.canMove = false;
                menuCanvas.SetActive(true);
                gameCanvas.SetActive(false);
                break;
            case GameState.Cinematic:
                menuCanvas.SetActive(false);
                Debug.Log($"Game state changed to: {CurrentState}");
                StartCoroutine(cinematicManager.PlayCinematicAndWait(0));
                break;
            case GameState.InGame:
                firstQuestManager.StartQuest();
                playerStateManager.canMove = true;
                gameCanvas.SetActive(true);
                break;
            case GameState.EndGame:
                break;
            case GameState.PostGame:
                break;
        }
    }

    void Start()
    {
        SwitchState(GameState.Menu);
    }

    void Update()
    {
        
    }
}