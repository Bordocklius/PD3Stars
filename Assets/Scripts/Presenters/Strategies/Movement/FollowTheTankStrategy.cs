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
        private Transform _transform;

        public FollowTheTankStrategy(Brawler context, BrawlerPresenter contextPresenter) : base(context, contextPresenter)
        {
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}
