using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamController : MonoBehaviour
{
    #region Singleton
    public static FixedCamController Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private Transform _target;
    public Transform Target { get { return _target; } set { _target = value; } }

    private List<Transform> _assignedNodes = new List<Transform>();

    public void SetNodes(List<Transform> nodes)
    {
        _assignedNodes = nodes;
        SetCamera(0);
    }

    public void ClearNodes()
    {
        _assignedNodes.Clear();
    }

    public void SetCamera(int i)
    {
        if(i >= 0 && i < _assignedNodes.Count)
        {
            Camera.main.transform.SetPositionAndRotation(_assignedNodes[i].position, _assignedNodes[i].rotation);
        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.LookAt(_target.position);
    }
}
