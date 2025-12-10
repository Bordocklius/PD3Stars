using PD3Stars.Models;
using PD3Stars.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Strategies
{
    public interface IBrawlerStrategy
    {
        public Brawler Context { get; }
        public BrawlerPresenter ContextPresenter { get; }
        public void Update(float deltaTime);
        public void FixedUpdate(float fixedDeltaTime);
    }
}
