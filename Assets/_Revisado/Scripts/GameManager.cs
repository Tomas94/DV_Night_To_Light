using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public FixedPlayerModel playerModel;
    public UI_InGame playerUI;
    
    /*[SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject uiPlayer;
    
    private bool isPaused;
    */

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(gameObject);
    }



    /*private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            UpdateGameState();
            ShowPausePanel();
            NotShowUIPlayer();
        }
    }

    private void UpdateGameState()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            
        }
        else
        {
            Time.timeScale = 1f;
            
        }
    }

    private void ShowPausePanel()
    {
        if (isPaused)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }
    }

    private void NotShowUIPlayer()
    {
        if (isPaused)
        {
            uiPlayer.SetActive(false);
        }
        else
        {
            uiPlayer.SetActive(true);
        }
    }*/
}
