using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD3Stars.Models;

namespace PD3Stars.Models.ColtModels
{
    public partial class Colt
    {
        public class ColtPAFSM: BrawlerPAFSM
        {
            public new Colt Context {  get; set; }

            public ColtPAFSM(Colt context): base(context)
            {
                BrawlerPAExecutingState = new ColtPAExecutingState(this);
            }
        }
    }
    
}
