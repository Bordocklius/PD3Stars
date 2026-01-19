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
    public class FollowTheTankStrategy : MovementStrategyBase, IMovementStrategy
    {
        public Transform TargetTransform { get; set; }
        private Transform _transform;

        private float _followDistance = 1.5f;
        private float _tolerance = 0.2f;

        public FollowTheTankStrategy(Brawler context, BrawlerPresenter contextPresenter, Transform targetTransform) : base(context, contextPresenter)
        {            
            TargetTransform = targetTransform;
            _transform = ContextPresenter.Transform;
        }

        public override void Update(float deltaTime)
        {
            Vector3 tankPos = TargetTransform.position;
            Vector3 tankFwd = TargetTransform.forward;
            tankFwd.y = 0f;

            // Calculate target position based on tank position & forward vector
            Vector3 targetPos = tankPos - tankFwd.normalized * _followDistance;
            targetPos.y = _transform.position.y;

            // Calculate distance to target pos
            Vector3 targetDis = targetPos - _transform.position;
            targetDis.y = 0f;
            float distance = targetDis.magnitude;

            if(distance > _tolerance)
            {
                // Calculate direction to look towards the tank
                Vector3 direction = tankPos - _transform.position;
                direction.y = 0f;
                if(direction.sqrMagnitude > 0.001f)
                {
                    RotationDirection = direction.normalized;
                    ContextPresenter.RotateCharacter(RotationDirection);
                }

                ContextPresenter.MoveCharacter(targetDis.normalized);
            }
            else
            {
                // Rotate character to look towards tank
                RotationDirection = tankFwd.normalized;
                ContextPresenter.RotateCharacter(RotationDirection);
            }
        }
    }
}
