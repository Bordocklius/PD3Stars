using PD3Stars.Singleton;
using System;
using System.Collections.Generic;
using PD3Stars.Models.ColtModels;

namespace PD3Stars.Models
{
    public class PD3StarsGame: UnityModelBaseClass
    {
        private List<Brawler> _brawlers = new List<Brawler>();
        public event EventHandler<BrawlerSpawnedEventArgs> BrawlerSpawned;
        
        public void AddColt()
        {
            Colt colt = new Colt();
            _brawlers.Add(colt);
            if(_brawlers.Count == 1)
            {
                Singleton<HUD>.Instance.Brawler = colt;
            }
            OnBrawlerSpawned(colt);
        }

        public void AddBrawler(Brawler brawler)
        {
            if (_brawlers.Count == 1)
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
