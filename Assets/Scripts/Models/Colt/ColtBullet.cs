using System;
using System.Numerics;
using PD3Stars.Models;

namespace PD3Stars.Models.ColtModels
{
    public partial class ColtBullet : UnityModelBaseClass, ITeleportable
    {
        public event EventHandler TTLExpired;
        public event EventHandler<TeleportEventArgs> TeleportEvent;

        public float Damage { get; set; } = 10;

        public float MaxTTL { get; set; } = 1.9f;

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

        public bool IsTeleportable { get; set; } = true;

        public float TeleportCooldown { get; set; } = 0.5f;
        protected float _teleportCDTimer;

        private ColtBulletTeleportFSM _fsm;

        public ColtBullet()
        {
            _fsm = new ColtBulletTeleportFSM(this);
        }

        public void ResetBullet()
        {
            TTL = 0f;
            this.IsActive = true;
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            if(!this.IsActive)
                return;

            base.FixedUpdate(fixedDeltaTime);
            _fsm.FixedUpdate(fixedDeltaTime);
            if(TTL < MaxTTL)
            {
                TTL += fixedDeltaTime;
            }
        }

        protected void CountTeleportCDTimer(float fixedDeltaTime)
        {
            _teleportCDTimer += fixedDeltaTime;
        }

        private void OnTTLExpired()
        {
            TTLExpired?.Invoke(this, EventArgs.Empty);
            this.IsActive = false;
        }

        public void TeleportRequested(Vector3 newPosition)
        {
            _fsm.CurrentState.TeleportRequested(newPosition);                
        }

        public void TeleportBullet(Vector3 newPosition)
        {
            TeleportEvent?.Invoke(this, new TeleportEventArgs(newPosition));
        }

        //public void SetBulletDirection(Vector3 startpos, Vector3 target)
        //{
        //    BulletDirection = (target - startpos).normalized;
        //    BulletDirection.y = 0;
        //}
    }
}