using PD3Stars.Models;
using PD3Stars.Presenters;
using PD3Stars.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PD3Stars.Strategies.Movement
{
    public class UserMovementStrategy: MovementStrategyBase
    {
        private InputReaderSO _inputReader;
        
        private Transform _transform;

        private Vector3 _previousLookDirection = Vector3.forward;

        public UserMovementStrategy(Brawler context, BrawlerPresenter contextPresenter):
            base(context, contextPresenter)
        { }

        public void SetInputActions(InputReaderSO inputReader)
        {
            if (_inputReader != null)
                UnsubToActions();

            _inputReader = inputReader;
            //_inputActions = inputActions;
            SubToActions();
            CacheValuesFromContext();
        }

        private void SubToActions()
        {
            _inputReader.Move += Move_Performed;
        }

        private void UnsubToActions()
        {
            _inputReader.Move -= Move_Performed;
        }

        protected override void CacheValuesFromContext()
        {
            base.CacheValuesFromContext();
            _transform = ContextPresenter.transform;
        }

        private void Move_Performed(object sender, InputReaderEventArgs e)
        {
            if (e.Ctx.performed)
                MoveDirection = e.Ctx.ReadValue<Vector2>();
            else if(e.Ctx.canceled)
                MoveDirection = Vector2.zero;
        }

        public override void Update(float deltaTime)
        {
            UpdateLookDirection();
        }

        private void UpdateLookDirection()
        {
            // Look towards ground based on where mouse is positioned
            Vector3 targetPoint = GetMousePosition();

            // Direction from character to mouse hit point
            Vector3 lookDirection = targetPoint - _transform.position;
            lookDirection.y = 0f; // Prevent tilting up/down

            if (lookDirection.sqrMagnitude > 0.001f)
            {
                lookDirection = lookDirection.normalized;
                RotationDirection = lookDirection;
                _previousLookDirection = lookDirection;
            }
            else
                RotationDirection = _previousLookDirection;
        }
    }
}
