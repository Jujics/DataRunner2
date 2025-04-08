using UnityEngine;

public class AlwaysLookAt : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private float baseSize = 1f; 
    [SerializeField] private float minScale = 0.5f; 
    [SerializeField] private float maxScale = 5f;  
    private Vector3 _originalScale;
    private Transform _player;
    void Start()
    {
        if (questManager == null || questManager.Player == null)
        {
            Debug.LogError("Player reference missing!");
            enabled = false;
            return;
        }
        _player = questManager.Player.transform;
        _originalScale = transform.localScale; 
    }
    void LateUpdate()
    {
        
        Vector3 lookDir = transform.position - _player.position;
        transform.rotation = Quaternion.LookRotation(lookDir);
        
        float distance = Vector3.Distance(transform.position, _player.position);
        float scaleFactor = baseSize / distance; 
        float clampedScale = Mathf.Clamp(scaleFactor, minScale, maxScale);
    
        transform.localScale = _originalScale * clampedScale;
    }
}
