using System;
using System.Collections;
using System.Collections.Generic;
using Code.Helpers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = System.Object;
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
            //p.startColor = new Color32(255, 0, 0, 255);
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
                /*this bitch empty*/
            }

            //position tells us the position of the dandelion seed
            var temp = Random.Range(0, 10);
            if (temp < 4) SpawnNewDandelion(position);
            
            _enter[i] = p;
        }
        
        _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);
    }

    public void Blow(Quaternion rotationPlayer, float speed)
    {
        float _speed = speed / 40;
        if (_speed < 0.5) _speed = 0.5f;
        var velocityOverLifetimeModule = _ps.velocityOverLifetime;
        _playerRotation = rotationPlayer;
        velocityOverLifetimeModule.speedModifier = new ParticleSystem.MinMaxCurve(_speed - 0.8f, _speed + 0.8f);
        transform.rotation = rotationPlayer;
        //var mainModule = _ps.main;
        //mainModule.startLifetime = 15;
        //StartCoroutine(WaitDandelion());
    }

    private void SpawnNewDandelion(Vector3 position)
    {
        var temp = Instantiate(_dandelionPrefab, new Vector3(position.x, transform.position.y - 2f, position.z), Quaternion.identity); //
        temp.GetComponentInChildren<ParticleSystem>().collision.SetPlane(0, GameObject.Find("Terrain").transform);
        temp.GetComponentInChildren<ParticleSystem>().collision
            .SetPlane(0, GameObject.Find("Terrain_(0.00, 0.00, 1000.00)").transform);
        temp.GetComponentInChildren<ParticleSystem>().collision
            .SetPlane(0, GameObject.Find("Terrain_(1000.00, 0.00, 0.00)").transform);
        var velocityOverLifetimeModule = temp.GetComponentInChildren<ParticleSystem>().velocityOverLifetime;
        velocityOverLifetimeModule.speedModifier = new ParticleSystem.MinMaxCurve(0, 0);
        // temp.GetComponentInChildren<ParticleSystem>().trigger
        //     .SetCollider(0, GameObject.Find("Terrain").transform);
        // temp.GetComponentInChildren<ParticleSystem>().trigger
        //     .SetCollider(0, GameObject.Find("Terrain_(0.00, 0.00, 1000.00)").transform);
        // temp.GetComponentInChildren<ParticleSystem>().trigger
        //     .SetCollider(0, GameObject.Find("Terrain_(1000.00, 0.00, 0.00)").transform);
        temp.GetComponentInChildren<DandelionController>()._dandelionPrefab = _dandelionPrefab;
    }

    IEnumerator WaitDandelion()
    {
        //size seeds down
        yield return new WaitForSeconds(40f);
        Debug.Log("self destroy");
        GameObject.Destroy(gameObject);
    }
}