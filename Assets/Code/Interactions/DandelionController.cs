using System;
using System.Collections;
using System.Collections.Generic;
using Code.Helpers;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DandelionController : MonoBehaviour
{
    private ParticleSystem _ps;
    private ParticleSystem.EmitParams _emitParams;
    private List<ParticleSystem.Particle> _enter = new List<ParticleSystem.Particle>();

    private readonly int _numParticles = 64;
    private Quaternion _playerRotation;
    [SerializeField] private GameObject _dandelionPrefab;

    void Start()
    {
        Vector3[] positions = VectorModifiers.PointsOnSphere(64);
        float scaling = 0.075f;

        _emitParams = new ParticleSystem.EmitParams();
        _ps = GetComponent<ParticleSystem>();


        for (int i = 0; i < _numParticles; i++)
        {
            // Set the position of the emitted particle
            Vector3 particlePosition = positions[i] * scaling;
            _emitParams.position = particlePosition;
            _emitParams.rotation3D = VectorModifiers.CalculateRotation(particlePosition, Vector3.zero).eulerAngles * -1;
            _ps.Emit(_emitParams, 1);
        }
    }


    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        if (_ps == null) return;

        int numEnter = _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);

        // iterate through the particles which entered the trigger
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = _enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            //GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);


            Vector3 position;
            var position1 = gameObject.transform.position;
            
            position = VectorModifiers.PointRotation(position1 + p.position, position1, _playerRotation);
            //Debug.Log(_ps.trigger.GetCollider(i));
            
            try
            {
                position = new Vector3(position.x, _ps.trigger.GetCollider(i).transform.position.y, position.z);
            }
            catch
            {
                //oh no;
            }


            //position tells us the position of the dandelion seed
            var temp = Random.Range(0, 10);
            //Debug.Log(temp);
            if (temp < 4)
            {
                SpawnNewDandelion(position);
                //Debug.Log(position);
            }


            _enter[i] = p;
        }

        _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);
    }

    public void Blow(Quaternion rotationPlayer, float speed)
    {
        // Debug.Log("i'm being blown");

        float _speed = speed / 50;
        if (_speed < 1) _speed = 1;
        var velocityOverLifetimeModule = _ps.velocityOverLifetime;
        _playerRotation = rotationPlayer;
        velocityOverLifetimeModule.speedModifier = new ParticleSystem.MinMaxCurve(_speed - 0.8f, _speed + 0.8f);
        var rotationPlayerEuler = rotationPlayer.eulerAngles;
        var vectorQuaternion = new Vector3(-15, rotationPlayerEuler.y, rotationPlayerEuler.z);
        transform.rotation = rotationPlayer;
        //Quaternion.Euler(vectorQuaternion);
        //GetComponentsInParent<Transform>()[1].rotation = Quaternion.Euler(vectorQuaternion);
    }

    private void SpawnNewDandelion(Vector3 position)
    {
        // RaycastHit hit;
        // Ray ray = new Ray(position, Vector3.down);
        // Physics.Raycast(ray, out hit);
        // Debug.Log((hit.collider.transform.position));
        // position = hit.collider.transform.position;
        //Debug.Log("SpawnNew");
        var temp = Instantiate(_dandelionPrefab, new Vector3(position.x, transform.position.y- 0.5f, position.z), Quaternion.identity);
        temp.GetComponentInChildren<ParticleSystem>().collision.SetPlane(0, GameObject.Find("Terrain").transform);
        temp.GetComponentInChildren<ParticleSystem>().collision.SetPlane(0, GameObject.Find("Terrain_(0.00, 0.00, 1000.00)").transform);
        temp.GetComponentInChildren<ParticleSystem>().collision.SetPlane(0, GameObject.Find("Terrain_(1000.00, 0.00, 0.00)").transform);
        var velocityOverLifetimeModule = temp.GetComponentInChildren<ParticleSystem>().velocityOverLifetime;
        velocityOverLifetimeModule.speedModifier = new ParticleSystem.MinMaxCurve(0,0);
        temp.GetComponentInChildren<ParticleSystem>().trigger
            .SetCollider(0, GameObject.Find("Terrain").transform);
        temp.GetComponentInChildren<ParticleSystem>().trigger
            .SetCollider(0, GameObject.Find("Terrain_(0.00, 0.00, 1000.00)").transform);
        temp.GetComponentInChildren<ParticleSystem>().trigger
            .SetCollider(0, GameObject.Find("Terrain_(1000.00, 0.00, 0.00)").transform);
        temp.GetComponentInChildren<DandelionController>()._dandelionPrefab = _dandelionPrefab;
        
        
        //If we want the dandelions to grow from the floor
        // temp.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        // for (int i = 0; i < 99; i++)
        // {
        //     
        //     
        //     StartCoroutine(Wait(0.5f , temp));
        // }
        // //Debug.Log("spawn new");
    }
    
    // IEnumerator Wait(float time, GameObject temp)
    // {
    //     
    //     yield return new WaitForSecondsRealtime(time);
    //     temp.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
    //     
    // }
}