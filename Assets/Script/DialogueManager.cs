using System;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueData
    {
        public string[] DialSeq1;
        public string[] DialSeq2;
        public string[] DialSeq3;
        public string[] DialSeq4;
    }
    private DialogueData _dialogueData;

    private void Start()
    {
        LoadDialogue();
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
        
    }
    
    
    
}
