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

    void ActivateLastSwitch()
    {
        ActivateSwitchButton();
        if (puzzle.ActualScore >= puzzle.NeededScore)
        {
            TryGetComponent<LightInteractable>(out LightInteractable _interactable);
            _interactable.ActivateInteraction();
        }
        else { return; }
    }

    public void Interact()
    {
        if (_isActive) return;
        if (_type == ButtonType.OneWay) ActivateOneWayButton();
        else if(_type == ButtonType.Last) ActivateLastSwitch();
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
    Switch,
    Last
}