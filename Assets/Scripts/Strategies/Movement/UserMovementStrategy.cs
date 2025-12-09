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
        public UserMovementStrategy(Brawler context, BrawlerPresenter<Brawler> contextPresenter):
            base(context, contextPresenter)
        { }

        private void OnMove(InputValue value)
        {
            MoveDirection = value.Get<Vector2>();
        }
    }
}
