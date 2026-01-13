using System;
using PD3Stars.Models;

namespace PD3Stars.Models.ColtModels
{
    public class ColtBullet : UnityModelBaseClass
    {
        public event EventHandler TTLExpired;

        public float Damage { get; set; } = 10;

        public float MaxTTL { get; set; } = 2f;

        private float _ttl;
        public float TTL

        {
            get { return _ttl; }
            set
            {
                if (_ttl == value)
                    return;

                _ttl = value;
                if (_ttl >= MaxTTL)
                {
                    OnPropertyChanged();
                    this.IsActive = false;
                }
                    
            }
        }

        public void ResetBullet()
        {
            TTL = 0f;
            this.IsActive = true;
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            if(this.IsActive)
                return;

            base.FixedUpdate(fixedDeltaTime);
            if(TTL < MaxTTL)
            {
                TTL += fixedDeltaTime;
            }
        }

        private void OnTTLExpired()
        {
            TTLExpired?.Invoke(this, EventArgs.Empty);
            this.IsActive = false;
        }

        //public void SetBulletDirection(Vector3 startpos, Vector3 target)
        //{
        //    BulletDirection = (target - startpos).normalized;
        //    BulletDirection.y = 0;
        //}
    }
}