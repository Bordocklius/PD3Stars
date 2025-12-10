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
        public UserMovementStrategy(Brawler context, BrawlerPresenter contextPresenter):
            base(context, contextPresenter)
        { }

        public void SetInputActions(InputSystem_Actions inputActions)
        {
            DisableActions();
            _inputActions = inputActions;
            EnableActions();
        }

        private void EnableActions()
        {
            _moveAction.Enable();
            _moveAction.performed += Move_Performed;
            _moveAction.canceled += ctx => MoveDirection = Vector2.zero;
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
    }
}
