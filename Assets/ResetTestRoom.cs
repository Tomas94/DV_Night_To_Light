using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetTestRoom : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    SceneManager.LoadScene("PruebasPrincipal");
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("MenuInicial");
        }         
    }
}
