using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandelionController : MonoBehaviour
{
    //private ParticleSystem _particleSystem;
    // Start is called before the first frame update

    private ParticleSystem _ps;

    public int numParticles = 64;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> _enter = new List<ParticleSystem.Particle>();
    private ParticleSystem.EmitParams _emitParams;
    

    private Quaternion _playerRotation;


    void Start()
    {
        Vector3[] positions = PointsOnSphere(64);

        _emitParams = new ParticleSystem.EmitParams();
        _ps = GetComponent<ParticleSystem>();

        float scaling = 0.075f;

        for (int i = 0; i < numParticles; i++)
        {
            // Set the position of the emitted particle
            Vector3 particlePosition = positions[i] * scaling;
            _emitParams.position = particlePosition;

            _emitParams.rotation3D = CalculateRotation(particlePosition, Vector3.zero).eulerAngles * -1;
            
            _ps.Emit(_emitParams, 1);
        }
    }

    private Vector3 PointRotation(Vector3 P1, Vector3 P2, Quaternion rot)
    {
        var v = P1 - P2; //the relative vector from P2 to P1.
        v = rot * v; //rotatate
        v = P2 + v; //bring back to world space

        return v;
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
            position = PointRotation(position1 + p.position, position1, _playerRotation);


            position = new Vector3(position.x, _ps.trigger.GetCollider(i).transform.position.y, position.z);
            
            ball.transform.position = position;

            ball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            _enter[i] = p;
            
        }

        _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);
    }
    public void Blow(Quaternion rotationPlayer)
    {
        Debug.Log("i'm being blown");
        _playerRotation = rotationPlayer;
        var velocityOverLifetimeModule = _ps.velocityOverLifetime;
        velocityOverLifetimeModule.speedModifier = new ParticleSystem.MinMaxCurve(0.05f,0.2f);
        GetComponentsInParent<Transform>()[1].rotation = rotationPlayer;

        
        


    }
    
    

    Vector3[] PointsOnSphere(int n)
    {
        List<Vector3> upts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / n;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = 0;
        float phi = 0;

        for (var k = 0; k < n; k++)
        {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            upts.Add(new Vector3(x, y, z));
        }

        Vector3[] pts = upts.ToArray();
        return pts;
    }

    public Quaternion CalculateRotation(Vector3 point1, Vector3 point2)
    {
        Vector3 direction = point2 - point1;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
        return rotation;
    }
}