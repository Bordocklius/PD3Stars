using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models.ElPrimoModels
{
    public partial class ElPrimo: Brawler
    {
        public override string PrefabName => "ElPrimoPrefab";

        public event EventHandler ElPrimoDashed;

        public float DashDamage;

        public ElPrimo(): base()
        {
            HPFSM = new ElPrimoHPFSM(this);
            PAFSM = new ElPrimoPAFSM(this);
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
            ElPrimoDashed?.Invoke(this, EventArgs.Empty);
        }

        public void PAEnded()
        {
            PAFSM.CurrentState.PAFinished();
        }
    }
}
