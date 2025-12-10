using PD3Stars.Models;
using PD3Stars.Presenters;
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
        private InputSystem_Actions _inputActions;
        private InputAction _moveAction;

        private Camera _camera;
        private LayerMask _groundMask;
        private Transform _transform;
        private float _rotationSpeed;

        private Vector3 _previousLookDirection = Vector3.forward;

        public UserMovementStrategy(Brawler context, BrawlerPresenter contextPresenter):
            base(context, contextPresenter)
        { }

        public void SetInputActions(InputSystem_Actions inputActions)
        {
            if(_inputActions != null)
                DisableActions();

            _inputActions = inputActions;
            EnableActions();
            CacheValuesFromContext();
        }

        private void EnableActions()
        {
            _moveAction = _inputActions.PlayerInput.Move;
            _moveAction.Enable();
            _moveAction.performed += Move_Performed;
            _moveAction.canceled += ctx => MoveDirection = Vector2.zero;
        }

        private void CacheValuesFromContext()
        {
            _camera = ContextPresenter.Camera;
            _transform = ContextPresenter.transform;
            _groundMask = ContextPresenter.GroundMask;
            _rotationSpeed = ContextPresenter.RotationSpeed;
        }

        private void DisableActions()
        {
            _moveAction.Disable();
            _moveAction.performed -= Move_Performed;
            _moveAction.canceled -= ctx => MoveDirection = Vector2.zero;
        }

        private void Move_Performed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                MoveDirection = ctx.ReadValue<Vector2>();
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

        private Vector3 GetMousePosition()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint = new Vector3();
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _groundMask))
            {
                targetPoint = hitInfo.point;
            }
            return targetPoint;
        }
    }
}
