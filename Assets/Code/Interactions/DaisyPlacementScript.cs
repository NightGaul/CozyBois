using System;
using System.Collections;
using System.Collections.Generic;
using Code.Interactions;
using UnityEngine;

public class DaisyPlacementScript : MonoBehaviour, IPedalPlacement
{
    private readonly int _numParticles = 12;
    private readonly float r = 0.001f;
    public void Placement(ParticleSystem.EmitParams emitParams, ParticleSystem ps)
    {
        emitParams.rotation3D = new Vector3(0, 0, 0);
        emitParams.position = new Vector3(emitParams.position.x-0.2f, emitParams.position.y, emitParams.position.z);
        for (int i = 0; i < _numParticles; i++)
        {
            float x = (float)(r * Math.Cos(45 * i));
            float y = (float)(r * Math.Sin(45 * i));
            
            // Set the position of the emitted particle
            
            emitParams.rotation3D = new Vector3(emitParams.rotation3D.x+30, emitParams.rotation3D.y, emitParams.rotation3D.z);
            
            ps.Emit(emitParams, 1);
        }
    }
}
