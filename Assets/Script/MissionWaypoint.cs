using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    [SerializeField]
    private Image img;
    private Transform target;
    public Transform Target { get { return target; } set { target = value; } }
    
    void Update()
    {
        if (target == null)
        {
            img.gameObject.SetActive(false);
            return;
        }
        else
        {
            if (!img.gameObject.activeSelf)
            {
                img.gameObject.SetActive(true);
            }
            img.transform.position = Camera.main.WorldToScreenPoint(target.position);
        }
    }
}
