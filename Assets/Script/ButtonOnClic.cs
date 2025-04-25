using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClic : MonoBehaviour
{
    [SerializeField]private Button buttonStart;
    [SerializeField]private Button buttonSkipCinematique;
    [SerializeField]private Button buttonSkipQuest;
    private void StartButtonOnClick()
    {
        GameManager.instance.SwitchState(GameManager.GameState.Cinematic);
    }

    private void ButtonSkipCinematiqueOnClick()
    {
        GameManager.instance.SwitchState(GameManager.GameState.InGame);
    }

    private void ButtonSkipQuestOnClick()
    {
        GameManager.instance.skipQuest = true;
        GameManager.instance.SwitchState(GameManager.GameState.InGame);
    }
    void Start()
    {
        buttonStart.onClick.AddListener(StartButtonOnClick);
        buttonSkipCinematique.onClick.AddListener(ButtonSkipCinematiqueOnClick);
        buttonSkipQuest.onClick.AddListener(ButtonSkipQuestOnClick);
    }
}
