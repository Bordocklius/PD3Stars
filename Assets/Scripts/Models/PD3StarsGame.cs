using PD3Stars.Singleton;
using System;
using System.Collections.Generic;
using PD3Stars.Models.ColtModels;
using PD3Stars.Models.ElPrimoModels;

namespace PD3Stars.Models
{
    public class PD3StarsGame: UnityModelBaseClass
    {
        public List<Brawler> Brawlers = new List<Brawler>();
        public event EventHandler<BrawlerSpawnedEventArgs> BrawlerSpawned;

        public void AddBrawler(Brawler brawler)
        {
            if (Brawlers.Count == 1)
            {
                Singleton<HUD>.Instance.Brawler = brawler;
            }
            OnBrawlerSpawned(brawler);
        }

        protected virtual void OnBrawlerSpawned(Brawler brawler)
        {
            BrawlerSpawned?.Invoke(this, new BrawlerSpawnedEventArgs(brawler));
        }
    }

    public class BrawlerSpawnedEventArgs : EventArgs
    {
        public Brawler Brawler { get; private set; }

        public BrawlerSpawnedEventArgs(Brawler newValue)
        {
            Brawler = newValue;
        }
    }

}
