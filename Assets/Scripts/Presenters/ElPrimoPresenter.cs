using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using PD3Stars.Models.ElPrimoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public class ElPrimoPresenter: BrawlerPresenter
    {      

        protected override void ModelSetInitialisation(Brawler previousModel)
        {
            base.ModelSetInitialisation(previousModel);
            if(previousModel != null )
            {
                ElPrimo previousElPrimo = previousModel as ElPrimo;
            }
        }

        public override void OnPrimaryAttack(Vector3 attackDirection)
        {
            Debug.Log("ElPrimo bonk");
        }
    }
}
