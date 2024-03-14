using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionController : MonoBehaviour
{
    private readonly List<Collider> _interactableObjects = new List<Collider>();

    [SerializeField] private Material _material;
    [SerializeField] private Material _default;
    [SerializeField] private Camera _main;
    private RaycastHit _current;

    private void Start()
    {
        
    }

    private void Update()
    {
        
        
            //careful here bcause of max distance: if you dont hit something else and dont have anything to hit in distance interactable object just stays
            RaycastHit hit;
            if (Physics.Raycast(_main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, 100f))
            {
                
                if (!_current.Equals(hit))
                {
                    try
                    {
                        _current.transform.gameObject.GetComponent<MeshRenderer>().material = _default;

                    }
                    catch
                    {
                        //i left this empty bc i can, fuck the system and clean code
                    }


                    if (hit.transform.gameObject.layer == 7)
                    {
                        _current = hit;
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material = _material;
                    }
                    else
                    {
                        _current = new RaycastHit();
                    }
                }
            }
        
        if (Input.GetKeyDown(KeyCode.E)) OnInteract();
    }

    private void OnInteract()
    {
        
            try
            {
                if (_current.transform.gameObject.layer == 7)
                {
                    Debug.Log("im cool");
                }

            }
            catch 
            {
                //i shouldnt do this but fuck this
            }
        
    }


    // void OnTriggerEnter(Collider other)
    // {
    //     _interactableObjects.Add(other);
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     _interactableObjects.Remove(other);
    // }


    // private void OnInteract()
    // {
    //     if (_interactableObjects.Count <= 0) return;
    //
    //
    //     //placeHolder Action to show interact works
    //     //ClosestObjectFromArray(_interactableObjects).gameObject.GetComponent<MeshRenderer>().material = _material;
    // }

    
    // private Collider ClosestObjectFromArray(List<Collider> list)
    // {
    //     Collider closest = list[0];
    //     foreach (var collider in list)
    //     {
    //         if (Vector3.Distance(transform.position, collider.gameObject.transform.position) <
    //             Vector3.Distance(transform.position, closest.gameObject.transform.position))
    //         {
    //             closest = collider;
    //         }
    //     }
    //
    //     return closest;
    // }
}