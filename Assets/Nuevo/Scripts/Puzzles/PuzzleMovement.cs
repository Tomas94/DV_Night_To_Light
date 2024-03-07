using System.Collections;
using UnityEngine;

public class PuzzleMovement : Puzzles
{
    [SerializeField] Vector3 _finishingPosition;

    protected override void PuzzleComplete()
    {
        base.PuzzleComplete();
        StartCoroutine(StartCompleteAnimation());
    }

    IEnumerator StartCompleteAnimation()
    {
        _isAnimPlaying = true;
        Vector3 initialPos = _startingPosition.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < _animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _animationDuration);
            Vector3 posicionInterpolada = Vector3.Lerp(initialPos, _finishingPosition, t);
            _barrier.localPosition = posicionInterpolada;
            yield return null;
        }

        _barrier.localPosition = _finishingPosition;
        yield return new WaitForSeconds(_waitingDuration);

        Destroy(_barrierCamera.gameObject);

        _mainCam.gameObject.SetActive(true);
        //Destroy(gameObject);
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