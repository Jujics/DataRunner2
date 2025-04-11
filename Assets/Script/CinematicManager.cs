using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private GameObject cinematic;
    [SerializeField] private GameObject[] Images;
    [SerializeField] private VideoPlayer[] videoPlayer;
    public VideoPlayer[] VideoPlayer { get => videoPlayer;}
    
    public IEnumerator PlayCinematicAndWait(int cinematicIndex)
    {
        Images[cinematicIndex].gameObject.SetActive(true);
        cinematic.SetActive(true);
        videoPlayer[cinematicIndex].Play();

        switch (cinematicIndex)
        {
            case 0:
                yield return new WaitForSeconds(11.5f);
                break;
            case 1:
                yield return new WaitForSeconds(27.5f);
                break;
        }
        
    
        cinematic.SetActive(false);
        Images[cinematicIndex].gameObject.SetActive(false);
    
        if (GameManager.instance.CurrentState != GameManager.GameState.InGame)
        {
            GameManager.instance.SwitchState(GameManager.GameState.InGame);
        }
    }
}
