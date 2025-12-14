using System;
using UnityEngine;
using PD3Stars.Models;

namespace PD3Stars.Models.ColtModels

{
    public class ColtBullet : UnityModelBaseClass
    {
        public event EventHandler TTLExpired;

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

        public Vector3 BulletDirection;

        public void ResetBullet()
        {
            TTL = 0f;
            BulletDirection = Vector3.zero;
            this.IsActive = true;
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
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

        public void SetBulletDirection(Vector3 startpos, Vector3 target)
        {
            BulletDirection = (target - startpos).normalized;
            BulletDirection.y = 0;
        }
    }
}