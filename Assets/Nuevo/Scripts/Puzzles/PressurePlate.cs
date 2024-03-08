using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] bool _correctPlate;
    [SerializeField] LayerMask _canPressMask;
    [SerializeField] Puzzles _puzzle;

    void Start()
    {
        
    }

    public void PressPlate()
    {
        _puzzle.ActualScore += 1;
    }

    public void UnpressPlate()
    {
         _puzzle.ActualScore -= 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if((_canPressMask & (1 << other.gameObject.layer)) != 0){
            if (_correctPlate) PressPlate();
            _puzzle.UpdatePuzzleStatus();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_canPressMask & (1 << other.gameObject.layer)) != 0)
        {
            if (_correctPlate)
            {
                UnpressPlate();
                _puzzle.UpdatePuzzleStatus();
            }
        }
    }

}
