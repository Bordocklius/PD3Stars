using UnityEngine;

namespace PD3Stars.Strategies.PA
{
    public interface IPAStrategy: IBrawlerStrategy
    {
        public Vector3 AttackDirection { get; }
    }
}
