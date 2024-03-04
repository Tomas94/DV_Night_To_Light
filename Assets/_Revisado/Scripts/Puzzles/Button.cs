using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public ButtonType _type;
    public Puzzles puzzle;

    [SerializeField] bool _isActive;

    private void Awake()
    {
        _isActive = false;
    }

    void ActivateOneWayButton()
    {
        _isActive = true;
        puzzle.ActualScore++;
    }

    void ActivateSwitchButton()
    {
        ActivateOneWayButton();
        StartCoroutine(SwitchButtonCD());
    }

    public void Interact()
    {
        if (_isActive) return;
        if (_type == ButtonType.OneWay) ActivateOneWayButton();
        else ActivateSwitchButton();
        puzzle.UpdatePuzzleStatus();
    }

    public IEnumerator SwitchButtonCD()
    {
        yield return new WaitForSeconds(.5f);
        puzzle.ActualScore--;
        _isActive = false;     
    }
}


public enum ButtonType
{
    OneWay,
    Switch
}