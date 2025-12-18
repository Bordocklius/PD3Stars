using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public interface IHUDProvider: INotifyPropertyChanged
    {
        public float Health { get; set; }
        public float PALoadTimer { get; set; }

        public float HealthProgress { get; }
        public float PALoadingProgress { get; }
    }
}
