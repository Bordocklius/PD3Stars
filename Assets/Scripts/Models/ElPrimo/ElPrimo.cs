using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models.ElPrimoModels
{
    public partial class ElPrimo: Brawler
    {
        public ElPrimo(): base()
        {
            HPFSM = new ElPrimoHPFSM(this);
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            base.FixedUpdate(fixedDeltaTime);
        }

        public override void PARequested()
        {
            throw new NotImplementedException();
        }

        protected override void PAExecuted()
        {
            throw new NotImplementedException();
        }
    }
}
