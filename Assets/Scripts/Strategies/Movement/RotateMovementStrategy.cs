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
        public RotateMovementStrategy(Brawler context, BrawlerPresenter<Brawler> contextPresenter):
            base(context, contextPresenter)
        {
        }

        private Vector2 _rotateVector = new Vector2(1, 0);
        public override Vector2 MoveDirection { get => _rotateVector; }
    }
}
