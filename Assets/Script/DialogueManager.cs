using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField] 
    private PlayerInput actionAsset;
    [System.Serializable]
    public class DialogueData
    {
        public string[] DialSeq1;
        public string[] DialSeq2;
        public string[] DialSeq3;
        public string[] DialSeq4;
        public string[] DialSeq5;
    }
    private DialogueData _dialogueData;

    private void Start()
    {
        LoadDialogue();
    }
    private void Awake()
    {
        actionAsset = new PlayerInput();
    }
    private void OnEnable()
    {
        actionAsset.Enable();
    }
    
    private void OnDisable()
    {
        actionAsset.Disable();
    }

    private void LoadDialogue()
    {
        string filepath = Application.dataPath + "/Script/Json/Dialogue.json";

        if (File.Exists(filepath))
        {
            string jsonData = File.ReadAllText(filepath);
            _dialogueData = JsonUtility.FromJson<DialogueData>(jsonData);
            if (_dialogueData == null)
            {
                Debug.LogError("Failed to deserialize dialogue data");
                return;
            }
            Debug.Log("Dialogue Loaded");
        }
        else
        {
            Debug.Log("Dialogue File not found at" + filepath);
        }
    }

    public void StartDialogue(int dialogueID)
    {
        switch (dialogueID)
        {
            case 1:
                StartCoroutine(StartDialogueCoroutine(_dialogueData.DialSeq1, GameManager.GameState.Duringlvl1));
                break;
            case 2:
                StartCoroutine(StartDialogueCoroutine(_dialogueData.DialSeq2, GameManager.GameState.Duringlvl2));
                break;
            case 3:
                StartCoroutine(StartDialogueCoroutine(_dialogueData.DialSeq3, GameManager.GameState.Duringlvl3));
                break;
            case 4:
                StartCoroutine(StartDialogueCoroutine(_dialogueData.DialSeq4, GameManager.GameState.Duringlvl4));
                break;
            case 5:
                StartCoroutine(StartDialogueCoroutine(_dialogueData.DialSeq5, GameManager.GameState.PostGame));
                break;
        }
    }

    private IEnumerator StartDialogueCoroutine(string[] dialText, GameManager.GameState nextState)
    {
        foreach (var line in dialText)
        {
            dialogueText.text = line + "\n";
            bool submitted = false;
            actionAsset.UI.Submit.performed += ctx => submitted = true;
            yield return new WaitUntil(() => submitted);
            submitted = false;
            actionAsset.UI.Submit.performed -= ctx => submitted = true;
        }
    }
    
    
    
}
