using System;
using System.Collections.Generic;
using System.Linq;

namespace PD3Stars.Models.ColtModels
{
    public partial class Colt : Brawler
    {       
        public event EventHandler<ColtFiredEventArgs> ColtFired;

        public int MagSize { get; set; } = 8;
        protected int CurrentMagSize { get; set; }

        public float FireDelay { get; private set; } = 0.1f;
        private float _fireTimer;

        public List<ColtBullet> BulletPool;

        public override string PrefabName => "ColtPrefab";

        public Colt(): base()
        {
            HPFSM = new ColtHPFSM(this);
            PAFSM = new ColtPAFSM(this);

            BulletPool = new List<ColtBullet>(MagSize);
            while(BulletPool.Count < MagSize)
            {
                BulletPool.Add(new ColtBullet());
            }
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            base.FixedUpdate(fixedDeltaTime);
        }

        public override void PARequested()
        {
            PAFSM.CurrentState.ExecutePA();
        }

        protected override void PAExecuted()
        {
            CurrentMagSize--;

            // Get bullet from pool, create one if none is available
            ColtBullet bullet = BulletPool.Where(x => !x.IsActive).FirstOrDefault();
            if(bullet == null)
            {
                bullet = new ColtBullet();
                bullet.IsActive = true;
                BulletPool.Add(bullet);
            }
            bullet.ResetBullet();
            ColtFired.Invoke(this, new ColtFiredEventArgs(bullet));
        }

    }

    public class ColtFiredEventArgs : EventArgs
    {
        public ColtBullet ColtBullet;

        public ColtFiredEventArgs(ColtBullet coltBullet)
        {
            ColtBullet = coltBullet;
        }
    }
}
