using Code.Helpers;
using UnityEngine;


namespace Code.Interactions
{
    public class DandelionPlacement : MonoBehaviour , IPedalPlacement
    {
        private readonly int _numParticles = 64;
        
        public void Placement(ParticleSystem.EmitParams emitParams, ParticleSystem ps)
        {
            Vector3[] positions = VectorModifiers.PointsOnSphere(64);
            float scaling = 0.075f;
            
            for (int i = 0; i < _numParticles; i++)
            {
                // Set the position of the emitted particle
                Vector3 particlePosition = positions[i] * scaling;
                emitParams.position = particlePosition;
                emitParams.rotation3D = VectorModifiers.CalculateRotation(particlePosition, Vector3.zero).eulerAngles * -1;
                ps.Emit(emitParams, 1);
            }
        }
        
            
    }
}