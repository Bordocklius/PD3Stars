using PD3Stars.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public class TeleporterPresenter: PresenterBaseClass<Portal>
    {
        [SerializeField]
        private TeleporterPresenter _destinationPortal;

        protected override void ModelSetInitialisation(Portal previousModel)
        {
            base.ModelSetInitialisation(previousModel);
            if(previousModel != null)
            {
                Model.PropertyChanged -= Model_OnPropertyChanged;
            }
            Model.PropertyChanged += Model_OnPropertyChanged;
        }

        protected override void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        private void Awake()
        {
            if(_destinationPortal  != null)
            {
                // Convert portal position to system.numerics vector3 to store in model
                System.Numerics.Vector3 destination = 
                    new System.Numerics.Vector3(_destinationPortal.transform.position.x, 0f, _destinationPortal.transform.position.z);
                Model = new Portal(destination);
            }
            else 
                Model = new Portal();
        }

        private void TryTeleport(ITeleportable teleportable)
        {
            if(Model.Destination != System.Numerics.Vector3.Zero)
            {
                Debug.Log("Teleported");
                teleportable.TeleportRequested(Model.Destination);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entered");
            if(other.TryGetComponent<IGetModel>(out IGetModel modelgetter))
            {
                Debug.Log("Found model");
                ITeleportable colliderModel = modelgetter.GetModel() as ITeleportable;
                if(colliderModel != null && colliderModel.IsTeleportable)
                {
                    Debug.Log("trying teleport");
                    TryTeleport(colliderModel);
                    //colliderModel.TeleportRequested(Model.Destination);
                }
            }
        }

    }
}
