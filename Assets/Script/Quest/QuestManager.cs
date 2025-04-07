using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    
    [SerializeField]
    private QuestType questType;
    [SerializeField]
    private string questName;
    public string QuestName { get => questName; }
    [SerializeField]
    private TMP_Text questText;
    public TMP_Text QuestText { get => questText; }

    #region Bus quest stuff
    
    [SerializeField]
    private GameObject startLine;
    [SerializeField]
    private GameObject endLine;
    [SerializeField]
    private GameObject player;
    
    #endregion
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public enum QuestType
    {
        BusRoute,
        OtherStuff
    }
}
