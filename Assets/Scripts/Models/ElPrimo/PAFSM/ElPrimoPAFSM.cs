using PD3Stars.Models.ColtModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD3Stars.Models.ColtModels.Colt;

namespace PD3Stars.Models.ElPrimoModels
{
   public partial class ElPrimo
    {
        public class ElPrimoPAFSM: BrawlerPAFSM
        {
            public new ElPrimo Context { get; set; }

            public ElPrimoPAFSM(ElPrimo context) : base(context)
            {
                BrawlerPAExecutingState = new ElPrimoPAExecutingState(this);
            }
        }
    }
}
