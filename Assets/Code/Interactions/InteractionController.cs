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
    private float _mouseValue;

    [SerializeField] private Camera _main;
    private RaycastHit _current;
    private float past;

    private void Start()
    {
        _mouseValue = 0;
        _main.fieldOfView = 60;
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

        // _main.fieldOfView += (Input.GetAxis("Mouse ScrollWheel") * -5 );
        if (Input.GetAxis("Mouse ScrollWheel") <= 0)
        {
            if (_mouseValue < 5)
            {
                _mouseValue -= Input.GetAxis("Mouse ScrollWheel");
                _main.fieldOfView += (Input.GetAxis("Mouse ScrollWheel") * -5);
            }
        }
        else
        {
            if (_mouseValue > 0.1) Release(_mouseValue);
            _mouseValue = 0;
            if (_main.fieldOfView > 60f)
            {
                _main.fieldOfView -= 1;
            }
        }
    }

    private void Release(float speed)
    {
        if (_current.transform.gameObject.layer == 7)
        {
            _current.transform.gameObject.GetComponentInChildren<DandelionController>()
                .Blow(gameObject.transform.rotation, speed);
        }
    }

    private void OnInteract()
    {
        try
        {
            if (_current.transform.gameObject.layer == 7)
            {
                _current.transform.gameObject.GetComponentInChildren<DandelionController>()
                    .Blow(gameObject.transform.rotation, 0.13f);
            }
        }
        catch
        {
            //i shouldnt do this but fuck this
        }
    }
}