using PD3Stars.Models;
using PD3Stars.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Strategies.Movement
{
    public class RotateMovementStrategy : MovementStrategyBase
    {
        private float _rotationSpeed = 90f;
        private float _angle = 0f;

        private float _yaw;

        public RotateMovementStrategy(Brawler context, BrawlerPresenter contextPresenter):
            base(context, contextPresenter)
        {
        }

        public override void Update(float deltaTime)
        {
            _yaw += ContextPresenter.RotationSpeed * deltaTime;
            float rad = _yaw * Mathf.Deg2Rad;
            RotationDirection = new Vector3(Mathf.Sin(rad), 0f, Mathf.Cos(rad));
        }

    }
}
