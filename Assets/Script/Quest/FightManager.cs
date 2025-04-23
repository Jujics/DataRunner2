using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public event Action OnPlayerDeath,OnEnemyDeath;

    [Header("MainStuff")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject fightUi;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject borderGameObject;
    [SerializeField] private string[] dialogueStart;
    [SerializeField] private string[] dialogueEnd;
    [SerializeField] private TMP_Text dialogueText;
    
    [Header("Enemy")] 
    [SerializeField] private int enemyPv;
    [SerializeField] private Slider enemyLifeBar;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawn;
    private EnemyController enemyController;
    private GameObject enemy;
    
    [Header("Player")]
    [SerializeField] private int playerPv;
    [SerializeField] private Slider playerLifeBar;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject gunCar;
    private GameObject player;
    private PlayerStateManager playerStateManager;
    
    
    
    
    public void BeforeFight(GameObject _player)
    {
        player = _player;
        playerStateManager = player.GetComponent<PlayerStateManager>();
        player.transform.position = playerSpawn.transform.position;
        enemy = Instantiate(enemyPrefab);
        enemy.transform.position = playerSpawn.transform.position;
        enemyController = enemy.GetComponent<EnemyController>();
        borderGameObject.SetActive(true);
        StartCoroutine(DialoguePhase(dialogueStart));
    }


    private IEnumerator DialoguePhase(string[] dialogue)
    {
        yield return new WaitForSeconds(2f);
        loadingScreen.SetActive(false);

        if (dialogue == null || dialogue.Length == 0)
        {
            Debug.LogError("No dialogue in dialStartList!");
            yield break;
        }

        foreach (var dial in dialogue)
        {
            dialogueText.text = dial;
            Debug.Log($"Displaying: {dial}");   
            yield return new WaitForSeconds(1f);
            WaitUntil waitUntil = new WaitUntil(() => 
            {
                bool isPressed = playerStateManager.actionAsset.Player.Dialogue.ReadValue<float>() > 0.5f;
                Debug.Log($"Dialogue button pressed: {isPressed}");
                return isPressed;
            });
        
            yield return waitUntil;
        }
        playerStateManager.canMove = true;
        Fight();
    }

    private void Fight()
    {
        
    }
    
    
}
