using System;
using System.Collections;
using System.Collections.Generic;
using Code.Helpers;
using UnityEngine;

public class DandelionController : MonoBehaviour
{
    private ParticleSystem _ps;
    private ParticleSystem.EmitParams _emitParams;
    private List<ParticleSystem.Particle> _enter = new List<ParticleSystem.Particle>();

    private readonly int _numParticles = 64;
    private Quaternion _playerRotation;

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
            GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);


            Vector3 position;
            var position1 = gameObject.transform.position;
            position = VectorModifiers.PointRotation(position1 + p.position, position1, _playerRotation);
            Debug.Log(_ps.trigger.GetCollider(i));
            try
            {
                position = new Vector3(position.x, _ps.trigger.GetCollider(i).transform.position.y, position.z);
            }
            catch
            {
                Debug.Log("oh no");
            }

            ball.transform.position = position;
            ball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            _enter[i] = p;
        }

        _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);
    }

    public void  Blow(Quaternion rotationPlayer, float speed)
    {
        Debug.Log("i'm being blown");

        float _speed = speed/50;
        if (_speed < 1) _speed = 1;
        var velocityOverLifetimeModule = _ps.velocityOverLifetime;
        _playerRotation = rotationPlayer;
        velocityOverLifetimeModule.speedModifier = new ParticleSystem.MinMaxCurve(_speed-0.8f, _speed+0.8f);
        GetComponentsInParent<Transform>()[1].rotation = rotationPlayer;
        
        
    }
}