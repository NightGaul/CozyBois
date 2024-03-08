using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class InteractionController : MonoBehaviour
{
    
    
    private readonly ArrayList _interactableObjects = new ArrayList();
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
    }
}
