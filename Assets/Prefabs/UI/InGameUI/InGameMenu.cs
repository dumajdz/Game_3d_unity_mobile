using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] Button ResumeBtn;
    [SerializeField] Button RestartBtn;
    [SerializeField] Button MainMenu;
    [SerializeField] UIManager uiManager;

    private void Start()
    {
        ResumeBtn.onClick.AddListener(ResumeGame);
        RestartBtn.onClick.AddListener(RestartGame);
        MainMenu.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu");
    }

    private void RestartGame()
    {
        Debug.Log("Restart level");
    }

    private void ResumeGame()
    {
        uiManager.SwithToGameplayUI();
        Debug.Log("Resume Game");
    }
}
