using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(gameObject);
    }

    public void PreloadLevel(string sceneName) => SceneManager.LoadSceneAsync(sceneName);
    public void LoadLevel(string sceneName) => SceneManager.LoadScene(sceneName);
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void ToMainMenu() => SceneManager.LoadScene(0);
    public void QuitGame() => Application.Quit();
}
