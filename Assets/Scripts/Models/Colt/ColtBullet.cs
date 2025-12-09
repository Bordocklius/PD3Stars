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
                    OnTTLExpired();
            }
        }

        public Vector3 BulletDirection;


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
        }

        public void SetBulletDirection(Vector3 startpos, Vector3 target)
        {
            BulletDirection = (target - startpos).normalized;
            BulletDirection.y = 0;
        }
    }
}