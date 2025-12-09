using UnityEngine;

namespace PD3Stars.Models
{
    public abstract class UnityModelBaseClass : ModelBaseClass
    {
		private bool _isActive;
		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				if (_isActive == value)
					return;

				_isActive = value;
				OnPropertyChanged();
			}
		}

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void FixedUpdate(float fixedDeltaTime)
        {

        }
    }

}
