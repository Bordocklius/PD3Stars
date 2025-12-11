using PD3Stars.Models;
using PD3Stars.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Strategies
{
    public class BrawlerStrategyBaseClass: IBrawlerStrategy 
    {
		private Brawler _context;
		public virtual Brawler Context
		{
			get { return _context; }
			set
			{
				if (_context == value)
					return;
				if(_context != null)
                    _context.PropertyChanged -= Context_OnPropertyChanged;

                _context = value;
				_context.PropertyChanged += Context_OnPropertyChanged;
			}
		}


		private BrawlerPresenter _contextPresenter;
		public virtual BrawlerPresenter ContextPresenter
		{
			get { return _contextPresenter; }
			set
			{
				if (_contextPresenter == value)
					return;
				_contextPresenter = value;
			}
		}

        public Camera Camera;
        public LayerMask GroundMask;

        public BrawlerStrategyBaseClass(Brawler context, BrawlerPresenter contextPresenter)
		{
			Context = context;
			ContextPresenter = contextPresenter;
		}

        protected virtual void CacheValuesFromContext()
        {
            Camera = ContextPresenter.Camera;
            GroundMask = ContextPresenter.GroundMask;
        }

        protected virtual void Context_OnPropertyChanged(object sender, PropertyChangedEventArgs e) { }

		public virtual void Update(float deltaTime) { }

		public virtual void FixedUpdate(float fixedDeltaTime) { }

        public Vector3 GetMousePosition()
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint = new Vector3();
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, GroundMask))
            {
                targetPoint = hitInfo.point;
            }
            return targetPoint;
        }
    }
}
