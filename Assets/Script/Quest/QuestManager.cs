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
    private string[] dialStartList;
    [SerializeField] 
    private string[] dialEndList;
    [SerializeField]
    private GameObject startLine;
    [SerializeField]
    private GameObject endLine;
    [SerializeField]
    private GameObject player;
    
    #endregion

    public void StartQuest()
    {
        switch (questType)
        {
            case QuestType.BusRoute:
                player.transform.position = startLine.transform.position;
                player.transform.rotation = startLine.transform.rotation;
                break;
            case QuestType.OtherStuff:
                break;
            default:
                break;
        }
    }

    public enum QuestType
    {
        BusRoute,
        OtherStuff
    }
}
