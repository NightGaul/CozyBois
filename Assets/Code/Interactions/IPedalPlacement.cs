using UnityEngine;

namespace Code.Interactions
{
    public interface IPedalPlacement
    {
        
        public void Placement(ParticleSystem.EmitParams emitParams, ParticleSystem ps);
    }
}