using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance{get; private set;}
    [SerializeField]
    private DialogueManager dialogueManager;

    public enum GameState
    {
        Menu,
        BeforeGame,
        Dialoguelvl1,
        Duringlvl1,
        Dialoguelvl2,
        Duringlvl2,
        Dialoguelvl3,
        Duringlvl3,
        Dialoguelvl4,
        Duringlvl4,
        PostGame,
        EndGame
    }
    public GameState CurrentState { get; private set; } 

    public void SwitchState(GameState newState)  
    {
        CurrentState = newState;
        Debug.Log($"Game state changed to: {newState}"); 

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
                break;
            case GameState.BeforeGame:
                break;
            case GameState.Dialoguelvl1:
                dialogueManager.StartDialogue(1);
                break;
            case GameState.Duringlvl1:
                break;
            case GameState.Dialoguelvl2:
                dialogueManager.StartDialogue(2);
                break;
            case GameState.Duringlvl2:
                break;
            case GameState.Dialoguelvl3:
                dialogueManager.StartDialogue(3);
                break;
            case GameState.Duringlvl3:
                break;
            case GameState.Dialoguelvl4:
                dialogueManager.StartDialogue(4);
                break;
            case GameState.Duringlvl4:
                break;
            case GameState.EndGame:
                dialogueManager.StartDialogue(5);
                break;
            case GameState.PostGame:
                break;
        }
    }

    void Start()
    {
        CurrentState = GameState.Menu;
    }

    void Update()
    {
        
    }
}