using System;
using System.Numerics;

namespace PD3Stars.Models
{
    public abstract partial class Brawler : UnityModelBaseClass, IHealthBar, IHUDProvider, ITeleportable
    {
        public event EventHandler BrawlerRevived;
        public event EventHandler<TeleportEventArgs> TeleportEvent;

        public bool IsTeleportable { get; set; } = true;

        public const float MAXHEALTH = 1000;
        private float _health;
        public float Health
        {
            get { return _health; }
            set
            {
                if (value > MAXHEALTH)
                {
                    value = MAXHEALTH;
                }

                float oldHealth = _health;
                _health = value;

                OnPropertyChanged();
            }
        }

        public float HealthProgress { get => Health / MAXHEALTH; }

        private float _paLoadTimer;
        public float PALoadTimer
        {
            get { return _paLoadTimer; }
            set
            {
                if (_paLoadTimer == value)
                    return;

                _paLoadTimer = value;
                OnPropertyChanged();
            }
        }

        public float PALoadingTime = 2f;
        public float PALoadingProgress { get => Math.Clamp(PALoadTimer / PALoadingTime, 0f, 1f) ; }

        protected BrawlerHPFSM HPFSM;

        public bool IsAlive => HPFSM.IsAlive;

        protected BrawlerPAFSM PAFSM;
        protected BrawlerHPChecker HPChecker;

        public virtual string PrefabName => "Brawler";

        public Brawler()
        {
            SetHPHalf();
            InitializeFSMs();
            InitializeHPChecker();
            IsTeleportable = true;
        }

        public abstract void InitializeFSMs();

        public void InitializeHPChecker()
        {
            HPChecker = new BrawlerHPChecker(this);
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            HPFSM.FixedUpdate(fixedDeltaTime);
            PAFSM.FixedUpdate(fixedDeltaTime);
        }

        public abstract void PARequested();

        protected abstract void PAExecuted();

        public void RegenerateHealth(float fixedDeltaTime)
        {
            if (Health >= MAXHEALTH) return;
            // Regenerate 13% of maxhealth/sec
            Health += fixedDeltaTime * 0.13f * MAXHEALTH;
        }

        public void CountPATimer(float fixedDeltaTime)
        {
            PALoadTimer += fixedDeltaTime;
        }

        public void ReceiveDamage(float damage)
        {
            HPFSM.CurrentState.TakeDamage(damage);
        }

        public void SetHPHalf()
        {
            Health = MAXHEALTH / 2;
        }

        public void OnHealthDepleted()
        {
            PAFSM.CurrentState.BrawlerDied();
            HPFSM.CurrentState.BrawlerDied();
        }
        public void OnHealthTookDamage()
        {
            HPFSM.CurrentState.BrawlerTookDamage();
        }

        public void HPFSM_OnBrawlerRevived()
        {
            SetHPHalf();
            PAFSM.CurrentState.BrawlerRevived();
            BrawlerRevived?.Invoke(this, EventArgs.Empty);
        }

        public void TeleportRequested(Vector3 newPosition)
        {
            if(IsTeleportable)
                TeleportEvent?.Invoke(this, new TeleportEventArgs(newPosition));
        }
    }

    public class BrawlerHealthEventArgs : EventArgs
    {
        public float OldHealth { get; set; }
        public float NewHealth { get; set; }

        public BrawlerHealthEventArgs(float oldHealth, float newHealth)
        {
            OldHealth = oldHealth;
            NewHealth = newHealth;
        }
    }
}
