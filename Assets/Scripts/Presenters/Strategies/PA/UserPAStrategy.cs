using PD3Stars.Models;
using PD3Stars.Presenters;
using PD3Stars.Strategies;
using PD3Stars.Strategies.PA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PD3Stars.Strategies.PA
{
    public class UserPAStrategy : PAStrategyBase
    {
        private InputSystem_Actions _inputActions;
        private InputAction _paAction;

        public UserPAStrategy(Brawler context, BrawlerPresenter contextPresenter) : base(context, contextPresenter)
        {
        }

        public void SetInputActions(InputSystem_Actions inputActions)
        {
            if (_inputActions != null)
                DisableActions();

            _inputActions = inputActions;
            EnableActions();
            CacheValuesFromContext();
        }

        private void EnableActions()
        {
            _paAction = _inputActions.PlayerInput.PrimaryAttack;
            _paAction.Enable();
            _paAction.performed += PA_Performed;
        }

        //protected override void CacheValuesFromContext()
        //{
        //    base.CacheValuesFromContext();
        //    _transform = ContextPresenter.transform;
        //    _rotationSpeed = ContextPresenter.RotationSpeed;
        //}

        private void DisableActions()
        {
            _paAction.Disable();
            _paAction.performed -= PA_Performed;
        }

        protected virtual void PA_Performed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                AttackDirection = GetMousePosition();
                ContextPresenter.OnPrimaryAttack(AttackDirection);
            }
        }
    }
}
