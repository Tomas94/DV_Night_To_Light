using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinternaHolder : MonoBehaviour
{
    [SerializeField] Transform Camera;



    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.transform.forward;
    }
}
