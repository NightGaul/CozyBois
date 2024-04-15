using System;
using System.Collections;
using System.Collections.Generic;
using Code.Helpers;
using Code.Interactions;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FlowerController : MonoBehaviour
{
    private ParticleSystem _ps;
    private ParticleSystem.EmitParams _emitParams;
    private List<ParticleSystem.Particle> _enter = new List<ParticleSystem.Particle>();

    
    private Quaternion _playerRotation;
    private IPedalPlacement _placement;
    [FormerlySerializedAs("_dandelionPrefab")] [SerializeField] private GameObject _flowerPrefab;
    

    void Start()
    {
        _emitParams = new ParticleSystem.EmitParams();
        _ps = GetComponent<ParticleSystem>();
        try
        {
            _placement = GetComponent<IPedalPlacement>();
        
            _placement.Placement(_emitParams, _ps);
        }
        catch 
        { 
            _ps.Emit(_emitParams, 1);
            
        }
        

        Physics.queriesHitBackfaces = true;
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
            
            Vector3 position;
            var position1 = gameObject.transform.position;

            position = VectorModifiers.PointRotation(position1 + p.position, position1, _playerRotation);
            

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
            if (temp < 4) SpawnNewPlant(position);

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
    }

    private void SpawnNewPlant(Vector3 position)
    {
        position = new Vector3(position.x, -40000, position.z);
        Physics.Raycast(position, Vector3.up, out var hit, float.PositiveInfinity);
        Debug.Log(hit.point);

        var temp = Instantiate(_flowerPrefab, hit.point + new Vector3(0, 0.5f, 0), Quaternion.identity); //
        temp.GetComponentInChildren<ParticleSystem>().collision.SetPlane(0, GameObject.Find("Terrain").transform);
        temp.GetComponentInChildren<ParticleSystem>().collision
            .SetPlane(0, GameObject.Find("Terrain_(0.00, 0.00, 1000.00)").transform);
        temp.GetComponentInChildren<ParticleSystem>().collision
            .SetPlane(0, GameObject.Find("Terrain_(1000.00, 0.00, 0.00)").transform);
        var velocityOverLifetimeModule = temp.GetComponentInChildren<ParticleSystem>().velocityOverLifetime;
        velocityOverLifetimeModule.speedModifier = new ParticleSystem.MinMaxCurve(0, 0);
        temp.GetComponentInChildren<FlowerController>()._flowerPrefab = _flowerPrefab;
    }

    IEnumerator WaitDandelion()
    {
        //size seeds down
        yield return new WaitForSeconds(40f);
        Debug.Log("self destroy");
        GameObject.Destroy(gameObject);
    }
}