using PD3Stars.Models;
using PD3Stars.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Strategies.PA
{
    public class PAStrategyBase : BrawlerStrategyBaseClass, IPAStrategy
    {
        public Vector3 AttackDirection {  get; set; }
        public PAStrategyBase(Brawler context, BrawlerPresenter contextPresenter) : base(context, contextPresenter)
        {
        }

    }
}
