using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    [SerializeField] Camera airCam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCamera(bool change)
    {
        if (change)
        {
            airCam.depth = 3;
        }
        else
        {
            airCam.depth = 0;
        }
    } 
}
