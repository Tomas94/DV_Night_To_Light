using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDeInicio : MonoBehaviour
{
   public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void Salir()
    {
        Debug.Log("Salir...");

        Application.Quit();
    }
}
