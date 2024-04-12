using System;
using System.Collections;
using System.Collections.Generic;
using Code.Interactions;
using UnityEngine;

public class SunflowerPlacement : MonoBehaviour, IPedalPlacement
{
    private readonly int _numParticles = 8;
    private readonly float scaling = 1f;
    private readonly float r = 0.01f;

    public void Placement(ParticleSystem.EmitParams emitParams, ParticleSystem ps)
    {
        //Vector3[] positions = VectorModifiers.PointsOnSphere(64);

        for (int i = 0; i < _numParticles; i++)
        {
            float x = (float)(r * Math.Cos(45 * i));
            float y = (float)(r * Math.Sin(45 * i)) + 0.01f;

            // Set the position of the emitted particle
            Vector3 particlePosition = new Vector3(x, y, -0.5f) * scaling;

            ;
            emitParams.rotation3D = new Vector3(emitParams.rotation3D.x, emitParams.rotation3D.y, 45 * i + 90);

            ps.Emit(emitParams, 1);
        }
    }
}