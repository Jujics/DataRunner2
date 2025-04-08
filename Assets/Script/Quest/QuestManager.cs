using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    
    [SerializeField]
    private QuestType questType;
    public QuestType _questType { get => questType; }
    [SerializeField]
    private string questName;
    public string QuestName { get => questName; }
    [SerializeField]
    private TMP_Text questText;
    public TMP_Text QuestText { get => questText; }
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private TMP_Text dialogueText;
    private bool isRunning;
    public bool IsRunning { get => isRunning; }

    #region Bus quest stuff

    [SerializeField] 
    private string[] dialStartList;
    [SerializeField] 
    private string[] dialEndList;
    [SerializeField]
    private GameObject startLine;
    [SerializeField]
    private GameObject endLine;
    [SerializeField]
    private GameObject player;
    public GameObject Player { get => player; }
    private PlayerStateManager playerStateManager;
    
    #endregion

    #region CameraSettings

    [SerializeField] 
    private CinemachineThirdPersonFollow playerCamera;
    private float carCameraDistance = 2f;
    private float carYShoulderOffset = 0.62f;
    private float busCameraDistance = 10f;
    private float busYShoulderOffset = 4.5f;

    #endregion

    void Start()
    {
        playerStateManager = player.GetComponent<PlayerStateManager>();
    }

    public void StartQuest()
    {
        switch (questType)
        {
            case QuestType.BusRoute:
                playerStateManager.canMove = false;
                loadingScreen.SetActive(true);
                isRunning = true;
                player.transform.position = startLine.transform.position;
                player.transform.rotation = startLine.transform.rotation;
                playerCamera.CameraDistance = busCameraDistance;
                playerCamera.ShoulderOffset.y = busYShoulderOffset;
                playerStateManager.vehicule[0].SetActive(false);
                playerStateManager.vehicule[1].SetActive(true);
                StartCoroutine(BusLoadQuest());
                break;
            case QuestType.OtherStuff:
                break;
            default:
                break;
        }
    }

    #region Bus quest logic
    public void BusPlayerArrived()
    {
        playerStateManager.canMove = false;
        loadingScreen.SetActive(true);
        StartCoroutine(BusEndQuest());
    }

    public IEnumerator BusLoadQuest()
    {
        Debug.Log("LoadQuest started");
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        loadingScreen.SetActive(false);

        if (dialStartList == null || dialStartList.Length == 0)
        {
            Debug.LogError("No dialogue in dialStartList!");
            yield break;
        }

        foreach (var dial in dialStartList)
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

        dialogueText.text = "LETS GO";
        yield return new WaitForSeconds(1f);
        dialogueText.text = "";
        playerStateManager.canMove = true;
        endLine.SetActive(true);
    
        Debug.Log("LoadQuest finished");
    }

    private IEnumerator BusEndQuest()
    {
        endLine.SetActive(false);
        yield return new WaitForSeconds(1f);
        loadingScreen.SetActive(false);
        foreach (var dial in dialEndList)
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
        loadingScreen.SetActive(true);
        playerStateManager.vehicule[0].SetActive(true);
        playerStateManager.vehicule[1].SetActive(false);
        playerCamera.CameraDistance = carCameraDistance;
        playerCamera.ShoulderOffset.y = carYShoulderOffset;
        yield return new WaitForSeconds(1f);
        loadingScreen.SetActive(false);
        playerStateManager.canMove = true;
        isRunning = false;
        playerStateManager.InQuest = false;
    }
    #endregion

    public enum QuestType
    {
        BusRoute,
        OtherStuff
    }
}
