using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
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
    public int EnemyPv
    {
        set => enemyPv = value;
        get => enemyPv;
    }
    
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private int playerPv;
    [SerializeField] private Slider playerLifeBar;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject gunCar;
    private PlayerStateManager playerStateManager;
    public int PlayerPv
    {
        set => playerPv = value; 
        get => playerPv; 
    }
    
    

    public void BeforeFight()
    {
        Debug.Log("StartFightQuest3");
        playerStateManager = player.GetComponent<PlayerStateManager>();
        player.transform.position = playerSpawn.transform.position;
        playerStateManager.fightManager = this;
        enemy = Instantiate(enemyPrefab);
        enemy.GetComponent<NavMeshAgent>().Warp(enemySpawn.transform.position);
        enemyController = enemy.GetComponent<EnemyController>();
        enemyController.fightManager = this;
        enemyController.Target = player.transform;
        borderGameObject.SetActive(true);
        Debug.Log("StartFightQuest4");
        StartCoroutine(DialoguePhase(dialogueStart, true));
    }


    private IEnumerator DialoguePhase(string[] dialogue, bool where)
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
        dialogueText.text = "";

        switch (where)
        {
            case true:
                Fight();
                break;
            case false:
                OnPlayerDeath?.Invoke();
                break;
        }
        
    }

    public void PlayerLooseHealth(int damage)
    {
        playerPv -= damage;
        playerLifeBar.value = playerPv;
        if (playerPv <= 0)
        {
            StartCoroutine(EndFight(false));
        }
    }
    
    public void EnemyLooseHealth(int damage)
    {
        enemyPv -= damage;
        enemyLifeBar.value = playerPv;
        if (enemyPv <= 0)
        {
            StartCoroutine(EndFight(true));
        }
    }

    private void Fight()
    {
        playerStateManager.canMove = true;
        gunCar.SetActive(true);
        fightUi.SetActive(true);
        enemyLifeBar.maxValue = enemyPv;
        enemyLifeBar.value = enemyPv;
        playerLifeBar.maxValue = playerPv;
        playerLifeBar.value = playerPv;
        enemyController.SwitchState(EnemyController.EnemyState.GettingCloser);
    }

    private IEnumerator EndFight(bool hasWon)
    {
        playerStateManager.canMove = false;
        gunCar.SetActive(false);
        fightUi.SetActive(false);
        loadingScreen.SetActive(true);
        player.transform.position = questManager.gameObject.transform.position;
        Destroy(enemy);
        yield return new WaitForSeconds(2f);
        switch (hasWon)
        {
            case true:
                loadingScreen.SetActive(false);
                //lose prefab set active
                yield return new WaitForSeconds(3f);
                borderGameObject.SetActive(false);
                StartCoroutine(DialoguePhase(dialogueEnd, false));
                break;
            case false:
                loadingScreen.SetActive(false);
                //lose prefab set active
                yield return new WaitForSeconds(3f);
                borderGameObject.SetActive(false);
                StartCoroutine(DialoguePhase(dialogueEnd, false));
                break;
        }
    }
    
    
    
    
}
