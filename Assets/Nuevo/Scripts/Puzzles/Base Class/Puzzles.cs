using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzles : MonoBehaviour
{
    [SerializeField] protected Transform _barrier;
    [SerializeField] protected Camera _barrierCamera;
    protected Camera _mainCam;

    //Posicion inicial de la barrera
    protected Transform _startingPosition;

    //Duracion animacion
    [SerializeField] protected float _animationDuration;
    [SerializeField] protected float _waitingDuration;

    [SerializeField] int _actualScore;
    [SerializeField] int _neededScore;

    protected bool _isAnimPlaying;

    public int ActualScore { get { return _actualScore; } set { _actualScore = value; } }
    public int NeededScore { get { return _neededScore; } set { _neededScore = value; } }

    void Awake()
    {
        _isAnimPlaying = false;
        _actualScore = 0;
        _mainCam = Camera.main;
        _startingPosition = _barrier;
    }

    public void UpdatePuzzleStatus()
    {
        if (ActualScore >= NeededScore && !_isAnimPlaying) PuzzleComplete();
    }

    protected virtual void PuzzleComplete()
    {
        CompletedPuzzleCamChange();
    }

    protected void CompletedPuzzleCamChange()
    {
        _mainCam.gameObject.SetActive(false);
        _barrierCamera.gameObject.SetActive(true);
    }
}
