using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Strategies.Movement
{
    public interface IMovementStrategy: IBrawlerStrategy
    {
        public Vector2 MoveDirection { get; }
        public Vector3 RotationDirection { get; }
    }
}
