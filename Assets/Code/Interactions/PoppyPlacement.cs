using System;
using Code.Interactions;
using UnityEngine;

public class PoppyPlacement : MonoBehaviour, IPedalPlacement
{
    private readonly int _numParticles = 8;
    private readonly float r = 0.001f;
    public void Placement(ParticleSystem.EmitParams emitParams, ParticleSystem ps)
    {
        for (int i = 0; i < _numParticles; i++)
        {
            float x = (float)(r * Math.Cos(45 * i));
            float y = (float)(r * Math.Sin(45 * i));
            
            // Set the position of the emitted particle
            
            emitParams.position = new Vector3(emitParams.position.x, -0.8f, emitParams.position.z);
            emitParams.rotation3D = new Vector3(65, emitParams.rotation3D.y-45, 55);
            
            ps.Emit(emitParams, 1);
        }
    }
}
