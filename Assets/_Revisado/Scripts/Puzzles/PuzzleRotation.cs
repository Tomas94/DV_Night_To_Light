using System.Collections;
using UnityEngine;

public class PuzzleRotation : Puzzles
{
    Quaternion _startingRotation;
    [SerializeField] Quaternion _finishingRotation;

    void Start()
    {
        _startingRotation = _startingPosition.rotation;
//        _finishingRotation = Quaternion.Euler(0, 135, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActualScore = NeededScore;
            UpdatePuzzleStatus();
        }
    }

    protected override void PuzzleComplete()
    {
        base.PuzzleComplete();
        StartCoroutine(StartCompleteAnimation());
    }

    IEnumerator StartCompleteAnimation()
    {
        Debug.Log("Inicia la corrutina");
        _isAnimPlaying = true;

        float elapsedTime = 0f;

        while (elapsedTime < _animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _animationDuration);
            Quaternion rotacionInterpolada = Quaternion.Lerp(_startingRotation, _finishingRotation, t);
            _barrier.rotation = rotacionInterpolada;
            yield return null;
        }
        
        _barrier.rotation = _finishingRotation;

        yield return new WaitForSeconds(_waitingDuration);
        _mainCam.enabled = true;
        
        Destroy(_barrierCamera.gameObject);
        Destroy(gameObject);
    }
}