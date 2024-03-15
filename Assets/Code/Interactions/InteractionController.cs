using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using Image = UnityEngine.UI.Image;


public class InteractionController : MonoBehaviour
{
    private readonly List<Collider> _interactableObjects = new List<Collider>();

    [SerializeField] private Sprite _interactUI;
    [SerializeField] private Sprite _default;
    
    [SerializeField] private Image _image;
    
    
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
                        _image.sprite = _default;
                        //_current.transform.gameObject.GetComponent<MeshRenderer>().material = _default;

                    }
                    catch
                    {
                        //i left this empty bc i can, fuck the system and clean code
                    }


                    if (hit.transform.gameObject.layer == 7)
                    {
                        _current = hit;
                        _image.sprite = _interactUI;
                        //hit.transform.gameObject.GetComponent<MeshRenderer>().material = _interactUI;
                        
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
        
            
                if (_current.transform.gameObject.layer == 7)
                {
                    //Debug.Log("im cool");
                    _current.transform.gameObject.GetComponentInChildren<DandelionController>().Blow();
                   

                }

            // }
            // catch 
            // {
            //     //i shouldnt do this but fuck this
            // }
        
    }

}