using System;
using UnityEngine;
using PD3Stars.Models;

namespace PD3Stars.Models.ColtModels
{
    public partial class Colt : Brawler
    {       
        public event EventHandler<ColtFiredEventArgs> ColtFired;

        public int MagSize { get; set; } = 8;
        public float FireDelay { get; private set; } = 0.1f;
        private float _fireTimer;

        public Colt(): base()
        {
            HPFSM = new ColtHPFSM(this);
            PAFSM = new ColtPAFSM(this);
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
            ColtFired.Invoke(this, new ColtFiredEventArgs(new ColtBullet()));
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
