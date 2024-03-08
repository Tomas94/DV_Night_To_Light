using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitPoint : MonoBehaviour
{
    [SerializeField] string _nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Entity>(out Entity player))
        {
            SceneController.Instance.PreloadLevel(_nextLevel);
        }
    }
}
