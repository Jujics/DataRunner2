using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class FristQuestManager : MonoBehaviour
{
    [SerializeField]
    private PlayerStateManager player;
    [SerializeField]
    private CinematicManager cinematic;
    [SerializeField]
    private GameObject startFightTrigger , fightCanvas;
    [SerializeField]
    private MissionWaypoint wpManager;
    [SerializeField] 
    private int playerPv;

    [Header("Enemy Stats")] [SerializeField]
    private string enemyName;
    [SerializeField] 
    private Sprite enemySprite;
    [SerializeField] 
    private Image enemyImage;
    [SerializeField]
    private int enemyPv;
    [SerializeField] 
    private int enemyAttackLow, enemyAttackHigh;

    [Header("UI")]

    [SerializeField]
    private Slider playerHealthBar;
    [SerializeField] 
    private Slider enemyHealthBar;
    [SerializeField] 
    private TMP_Text enemyNameDisplay;
    
    
    private void Start()
    {
        
    }

    public void StartQuest()
    {
        player.InQuest = true;
        wpManager.Target = startFightTrigger.transform;
    }

    public void StartFight()
    {
        player.canMove = false;
        
    }

    private void Fight()
    {
        fightCanvas.SetActive(true);
        enemyImage.sprite = enemySprite;
        while (enemyPv > 0 || playerPv > 0)
        {
            
        }
    }
}
