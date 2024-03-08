using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class InteractionController : MonoBehaviour
{
    
    
    private readonly List<Collider> _interactableObjects = new List<Collider>();

    [SerializeField] private Material _material;
    private void Update()
    {
        //Bruder i struggle mit input
        if(Input.GetKeyDown(KeyCode.E)) OnInteract();
    }

    void OnTriggerEnter(Collider other)
    {
        _interactableObjects.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _interactableObjects.Remove(other);
    }

    
    private void OnInteract()
    {
        if (_interactableObjects.Count <= 0) return;

        
        
        //placeHolder Action to show interact works
        ClosestObjectFromArray(_interactableObjects).gameObject.GetComponent<MeshRenderer>().material = _material;

    }

    private Collider ClosestObjectFromArray(List<Collider> list)
    {
        Collider closest = list[0];
        foreach (var collider in list)
        {
            
            
            if (Vector3.Distance(transform.position, collider.gameObject.transform.position) < Vector3.Distance(transform.position, closest.gameObject.transform.position))
            {
                closest = collider;
            }
        }
        
        return closest;
    }
        
    }
    

