using PD3Stars.Models;
using PD3Stars.Presenters;
using PD3Stars.ScriptableObjects;
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
        private InputReaderSO _inputReader;

        public UserPAStrategy(Brawler context, BrawlerPresenter contextPresenter) : base(context, contextPresenter)
        {
        }

        public void SetInputActions(InputReaderSO inputReader)
        {
            if (_inputReader != null)
                UnsubToActions();

            _inputReader = inputReader;
            SubToActions();
            CacheValuesFromContext();
        }

        private void SubToActions()
        {
            _inputReader.PA += PA_Performed;
        }

        private void UnsubToActions()
        {
            _inputReader.PA -= PA_Performed;
        }

        protected virtual void PA_Performed(object sender, InputReaderEventArgs e)
        {
            if (e.Ctx.performed)
            {
                AttackDirection = GetMousePosition();
                ContextPresenter.OnPrimaryAttack();
            }
        }

        //protected virtual void PA_Performed(InputAction.CallbackContext ctx)
        //{
        //    if (ctx.performed)
        //    {
        //        AttackDirection = GetMousePosition();
        //        ContextPresenter.OnPrimaryAttack(AttackDirection);
        //    }
        //}
    }
}
