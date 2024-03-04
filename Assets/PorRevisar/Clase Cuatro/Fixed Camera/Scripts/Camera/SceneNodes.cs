using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNodes : MonoBehaviour
{
    [Header("Scene Nodes")]
    [SerializeField] private List<Transform> _nodes = new List<Transform>();

    private void Start()
    {
        FixedCamController.Instance.SetNodes(_nodes);
    }
}
