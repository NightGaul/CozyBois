using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandelionController : MonoBehaviour
{
    //private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    
    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> _enter = new List<ParticleSystem.Particle>();

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        
        
                
    }

    public void Blow()
    {
        ps.Play();
        Debug.Log("i'm being blown");
    }

    // Update is called once per frame
    

    
    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        if(ps == null)return;
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);

        

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = _enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
            //ball.transform.SetParent(ps.trigger.GetCollider(i).transform); 
            Debug.Log("Ballpos: " + ball.transform.position);
            
            ball.transform.position = gameObject.transform.position + p.position;
            Debug.Log("Ballpos 2: " + ball.transform.position);
            ball.transform.position = new Vector3(ball.transform.position.x, ps.trigger.GetCollider(i).transform.position.y,ball.transform.position.z);
            
            
            Debug.Log(p.position);
            ball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                
            _enter[i] = p;
        }

        
        
        

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);
        
    }
    
    
}
