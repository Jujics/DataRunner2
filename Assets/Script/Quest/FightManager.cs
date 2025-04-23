using System;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public event Action OnPlayerDeath,OnEnemyDeath;

    [Header("MainStuff")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject fightUi;
    [SerializeField] private QuestManager questManager;
    
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
    
    
    
    
    public void Fight(GameObject _player)
    {
        _player = player;
        player.transform.position = playerSpawn.transform.position;
        enemy = Instantiate(enemyPrefab);
        enemy.transform.position = playerSpawn.transform.position;
        enemyController = enemy.GetComponent<EnemyController>();
        
    }
}
