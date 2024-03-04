using System.Collections;
using UnityEngine;

public class PuzzleMovement : Puzzles
{
    protected override void PuzzleComplete()
    {
        base.PuzzleComplete();
        StartCoroutine(StartCompleteAnimation());
    }

    IEnumerator StartCompleteAnimation()
    {
        _isAnimPlaying = true;
        Vector3 initialPos = _startingPosition.position;
        float elapsedTime = 0f;

        while (elapsedTime < _animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _animationDuration);
            Vector3 posicionInterpolada = Vector3.Lerp(initialPos, _finishingPosition, t);
            _barrier.position = posicionInterpolada;
            yield return null;
        }

        _barrier.position = _finishingPosition;
        yield return new WaitForSeconds(_waitingDuration);
        _mainCam.enabled = true;

        Destroy(_barrierCamera.gameObject);
        Destroy(gameObject);
    }

    #region Pruebas
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActualScore = NeededScore;
            UpdatePuzzleStatus();
        }
    }
    #endregion
}