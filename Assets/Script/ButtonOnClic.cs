using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClic : MonoBehaviour
{
    public Button buttonStart;
    private void StartButtonOnClick()
    {
        GameManager.instance.SwitchState(GameManager.GameState.Cinematic);
    }
    void Start()
    {
        buttonStart.onClick.AddListener(StartButtonOnClick);
    }
}
