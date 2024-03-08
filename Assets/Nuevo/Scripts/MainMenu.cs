using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> _Screens = new List<GameObject>();

    private void Start()
    {
        SelectScreen(_Screens[0]);
    }

    public void SelectScreen(GameObject _screen)
    {
        foreach (GameObject screen in _Screens)
        {
            if (screen == _screen) screen.SetActive(true);
            else screen.SetActive(false);
        }
    }
}
