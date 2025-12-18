using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public class HUD
    {
        public Brawler Brawler { get; set; }

        public IHUDProvider[] HUDProviders = new IHUDProvider[3];

        public HUD() { }

        public void AddHUDProvider(IHUDProvider provider)
        {

        }
    }
}
