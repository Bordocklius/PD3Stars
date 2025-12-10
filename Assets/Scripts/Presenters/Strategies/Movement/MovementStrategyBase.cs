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
    public abstract class MovementStrategyBase: BrawlerStrategyBaseClass, IMovementStrategy
    {

		private Vector2 _moveDirection;
		public virtual Vector2 MoveDirection
		{
			get { return _moveDirection; }
			set
			{
				if (_moveDirection == value)
					return;
				_moveDirection = value;
			}
		}


		private Vector3 _rotationDirection;
		public Vector3 RotationDirection
		{
			get { return _rotationDirection; }
			set
			{
				if (_rotationDirection == value)
					return;
				_rotationDirection = value;
			}
		}

		public MovementStrategyBase(Brawler context, BrawlerPresenter contextPresenter): 
			base(context, contextPresenter)
		{ }
    }
}
