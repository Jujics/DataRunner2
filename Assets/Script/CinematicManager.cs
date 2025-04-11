using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private GameObject cinematic;
    [SerializeField] private VideoPlayer[] videoPlayer;

    public void PlayCinematic(int cinematicIndex)
    {
        cinematic.SetActive(true);
        StartCoroutine(WaitForCinematic(cinematicIndex));
    }

    private IEnumerator WaitForCinematic(int cinematicIndex)
    {
        videoPlayer[cinematicIndex].Play();
        while (videoPlayer[cinematicIndex].isPlaying)
        {
            yield return null;
        }
        cinematic.SetActive(false);
        if (GameManager.instance.CurrentState != GameManager.GameState.InGame)
        {
            GameManager.instance.SwitchState(GameManager.GameState.InGame);
        }
    }
}
