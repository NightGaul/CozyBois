using System;
using System.Collections;
using System.Collections.Generic;
using Code.Interactions;
using UnityEngine;

public class PurplePlacementScript : MonoBehaviour , IPedalPlacement
{
    private readonly int _numParticles = 8;
    public void Placement(ParticleSystem.EmitParams emitParams, ParticleSystem ps)
    {
        for (int i = 0; i < _numParticles; i++)
        {
            emitParams.position = new Vector3(0,0,0);
            ps.Emit(emitParams, 1);
        }
    }
}
