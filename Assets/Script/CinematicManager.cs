using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CinematicManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cinematic;
    //[SerializeField]
    //private VideoClip[] cinematicClip;

    public void PlayCinematic(int cinematicIndex)
    {
        cinematic.SetActive(true);
        StartCoroutine(WaitForCinematic(cinematicIndex));
    }

    private IEnumerator WaitForCinematic(int cinematicIndex)
    {
        //play cinematique[cinematicIndex]
        yield return new WaitForSeconds(0.5f);
        cinematic.SetActive(false);
        GameManager.instance.SwitchState(GameManager.GameState.InGame);
    }
}
