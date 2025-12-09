using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using PD3Stars.Models.ElPrimoModels;
using PD3Stars.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Network
{
    public class NetworkInfo: MonoBehaviour
    {
        private float _timer;
        private int _currentSpawnCounter;

        [SerializeField]
        private float _spawnDelay = 3;
        [SerializeField]
        private int _maxSpawnCounter = 1;

        private void FixedUpdate()
        {
            SpawnBrawler();
        }

        private void SpawnBrawler()
        {
            if (_currentSpawnCounter > _maxSpawnCounter) return;
            _timer += Time.fixedDeltaTime;
            
            if(_timer >= _spawnDelay )
            {
                Brawler newBrawler = (_currentSpawnCounter % 2 == 0) ? new Colt() : new ElPrimo();
                Singleton<PD3StarsGame>.Instance.AddBrawler(newBrawler);
                _timer -= _spawnDelay;
                _currentSpawnCounter++;
            }
        }
    }
}
