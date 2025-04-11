using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstQuestManager : MonoBehaviour
{
    [SerializeField] private PlayerStateManager player;
    [SerializeField] private CinematicManager cinematic;
    [SerializeField] private GameObject startFightTrigger, fightCanvas, gameCanvas;
    [SerializeField] private MissionWaypoint wpManager;
    [SerializeField] private int playerPv;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private CinematicManager cinematicManager;

    [Header("Enemy Stats")] 
    [SerializeField] private string enemyName;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Image enemyImage;
    [SerializeField] private int enemyPv;
    [SerializeField] private int enemyAttackLow, enemyAttackHigh;

    [Header("UI")]
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider enemyHealthBar;
    [SerializeField] private TMP_Text enemyNameDisplay;
    [SerializeField] private TMP_Text combatLogText;
    [SerializeField] private float textDisplayTime = 2f;
    
    private Action currentTurnCompleteAction;

    [SerializeField]
    private bool playerTurn = true;
    private bool combatEnded = false;

    private void Start()
    {
        enemyNameDisplay.text = enemyName;
        playerHealthBar.maxValue = playerPv;
        playerHealthBar.value = playerPv;
        enemyHealthBar.maxValue = enemyPv;
        enemyHealthBar.value = enemyPv;
        
        fightCanvas.SetActive(false);
    }

    public void StartQuest()
    {
        player.InQuest = true;
        startFightTrigger.SetActive(true);
        wpManager.Target = startFightTrigger.transform;
    }

    public void StartFight()
    {
        wpManager.Target = null;
        player.canMove = false;
        gameCanvas.SetActive(false);
        StartCoroutine(Cinematic());
    }

    private void Fight()
    {
        fightCanvas.SetActive(true);
        enemyImage.sprite = enemySprite;
        StartCoroutine(CombatLoop());
    }

    private IEnumerator CombatLoop()
    {
        while (enemyPv > 0 && playerPv > 0 && !combatEnded)
        {
            Debug.Log(playerTurn);
            if (playerTurn)
            {
                yield return StartCoroutine(PlayerTurn());
            }
            else
            {
                Debug.Log("enemyTurn");
                yield return StartCoroutine(EnemyTurn());
            }
        }
        
        EndCombat();
    }

    private IEnumerator PlayerTurn()
    {
        SetButtonsInteractable(true);
        
        bool actionCompleted = false;
        currentTurnCompleteAction = () => actionCompleted = true;
        while (!actionCompleted)
        {
            yield return null;
        }
        
        SetButtonsInteractable(false);
        currentTurnCompleteAction = null;
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log($"Enemy go through" + playerTurn);
        int damage = UnityEngine.Random.Range(enemyAttackLow, enemyAttackHigh + 1);
        playerPv -= damage;
        playerPv = Mathf.Max(0, playerPv);
        playerHealthBar.value = playerPv;
        
        AddToCombatLog($"{enemyName} attacks for {damage} damage!");
        yield return new WaitForSeconds(textDisplayTime);
        if (playerPv <= 0)
        {
            combatEnded = true;
        }
        
        playerTurn = true;
    }
    public void OnAttackButton()
    {
        if (!playerTurn) return;
        
        int damage = UnityEngine.Random.Range(5, 11); 
        enemyPv -= damage;
        enemyPv = Mathf.Max(0, enemyPv);
        enemyHealthBar.value = enemyPv;
        
        AddToCombatLog($"You attack for {damage} damage!");
        playerTurn = false;
        StartCoroutine(ContinueAfterLog());
    }

    public void OnHealButton()
    {
        if (!playerTurn) return;
        
        int healAmount = 15;
        playerPv = Mathf.Min(playerPv + healAmount, (int)playerHealthBar.maxValue);
        playerHealthBar.value = playerPv;
        
        AddToCombatLog($"You heal for {healAmount} health!");
        playerTurn = false;
        StartCoroutine(ContinueAfterLog());
    }

    public void OnSpecialButton()
    {
        if (!playerTurn) return;
        
        if (UnityEngine.Random.value > 0.3f) 
        {
            int damage = UnityEngine.Random.Range(15, 21);
            enemyPv -= damage;
            enemyPv = Mathf.Max(0, enemyPv);
            enemyHealthBar.value = enemyPv;
            
            AddToCombatLog($"Special attack hits for {damage} damage!");
        }
        else
        {
            AddToCombatLog("Special attack missed!");
        }
        playerTurn = false;
        StartCoroutine(ContinueAfterLog());
    }

    private IEnumerator ContinueAfterLog()
    {
        yield return new WaitForSeconds(textDisplayTime);
        Debug.Log(playerTurn);
        currentTurnCompleteAction?.Invoke(); 
        playerTurn = false;
    }

    private void EndCombat()
    {
        if (playerPv <= 0)
        {
            AddToCombatLog("You were defeated!");
        }
        else if (enemyPv <= 0)
        {
            AddToCombatLog($"You defeated {enemyName}!");
            startFightTrigger.SetActive(false);
            player.InQuest = false;
            player.canMove = true;
        }
        
        StartCoroutine(CloseCombatCanvas());
    }

    private IEnumerator CloseCombatCanvas()
    {
        yield return new WaitForSeconds(textDisplayTime * 2);
        fightCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }


    private IEnumerator Cinematic()
    {
        cinematicManager.PlayCinematic(1);
        yield return new WaitForSeconds(10f);
        Fight();
    }
    

    private void AddToCombatLog(string message)
    {
        combatLogText.text = message;
    }

    private void SetButtonsInteractable(bool state)
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Button>().interactable = state;
        }
    }
}