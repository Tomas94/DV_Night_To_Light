using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRaycast : MonoBehaviour
{
    RaycastHit hit;
    //[SerializeField]Camera myCamera;
    //[SerializeField]PlayerController playerCont;
    [SerializeField] LayerMask interactable;
    //Ray ray;

    // Start is called before the first frame update
   //void Start()
   //{
   //    Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
   //    ray = myCamera.ScreenPointToRay(screenCenter);
   //    
   //}

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit , 7f, interactable))
        {
            if(hit.transform.tag == "Pickeable")
            {
                Debug.Log("Objeto detectado: " + hit.collider.name);
                Interactuable();
            }
            
        }
        Debug.DrawRay(transform.position, transform.forward*7f , Color.red);
    }
    
    
    void Interactuable()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(hit.transform.gameObject);
        }
    }
    
    
    // Update is called once per frame
    /*void Update()
    {
        
        
        Physics.Raycast(ray.origin, ray.direction, out hit, 50f, interactable);
        if ()
        {
            if(hit.transform.tag == "Puerta")
            {
                playerCont.OpenDoor();
            }
            Debug.Log(hit.transform.tag);
        /
        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 1f);
    }*/
}
