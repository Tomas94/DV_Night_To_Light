using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oclusion : MonoBehaviour
{
    /*Transform playerCam;

    float minDist;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OclusionDistance()
    {
        if(Vector3.Distance(playerCam.position, transform.position ) < minDist)
    }*/

   [SerializeField] GameObject player;
   [SerializeField] GameObject[] objetosScene;
   [SerializeField] List<GameObject> staticObjects;

    public float minDist;
    [SerializeField] int objetosOcultos;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        objetosScene = GameObject.FindObjectsOfType<GameObject>();
        staticObjects = new List<GameObject>();
        StaticFilter();
    }

    private void Update()
    {
        ObjectDistToPlayer();
    }

    void StaticFilter()
    {
        

        foreach (GameObject obj in objetosScene)
        {
            if (obj.isStatic)
            {
                staticObjects.Add(obj);
                
            }
        }
        objetosOcultos = staticObjects.Count;
        objetosScene = null;
    }

    void ObjectDistToPlayer()
    {
        int ocultos = 0;
        foreach (GameObject staticobj in staticObjects)
        {
            if(Vector3.Distance(staticobj.transform.position, player.transform.position) < minDist)
            {
                staticobj.SetActive(true);
                ocultos++;
            }
            else
            {
                staticobj.SetActive(false);

            }
        }
        objetosOcultos = ocultos;
    }
}
