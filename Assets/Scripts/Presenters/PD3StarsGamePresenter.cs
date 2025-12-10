using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using PD3Stars.Models.ElPrimoModels;
using PD3Stars.Singleton;
using PD3Stars.Strategies.Movement;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace PD3Stars.Presenters
{
    public class PD3StarsGamePresenter: PresenterBaseClass<PD3StarsGame>
    {
        public GameObject ColtPresenterPrefab;
        private ColtPresenter _coltPresenter;
        public PlayerInput PlayerInput;
        public InputSystem_Actions inputActions;
        public UIDocument HUD;
        public HUDPresenter HUDPresenter;

        private void Awake()
        {
            Model = Singleton<PD3StarsGame>.Instance;
            Model.AddColt();
        }

        protected override void ModelSetInitialisation(PD3StarsGame previousModel)
        {
            base.ModelSetInitialisation(previousModel);
            if(Model != null)
            {
                Model.BrawlerSpawned -= Model_BrawlerSpawned;
            }
            Model.BrawlerSpawned += Model_BrawlerSpawned;
        }

        protected override void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        private void Model_BrawlerSpawned(object sender, BrawlerSpawnedEventArgs e)
        {
            if(e.Brawler is Colt colt)
            {
                GameObject coltObj = Instantiate(ColtPresenterPrefab, transform);
                _coltPresenter = coltObj.GetComponent<ColtPresenter>();
                _coltPresenter.Model = colt;
                _coltPresenter.transform.position = new Vector3(0, 0, 0);
                _coltPresenter.transform.rotation = Quaternion.identity;
                _coltPresenter.gameObject.SetActive(true);
                IMovementStrategy movementStrategy = new UserMovementStrategy(_coltPresenter.Model, _coltPresenter);
                //_coltPresenter.AddPlayerInput(PlayerInput);
                _coltPresenter.AddHBPresenter();
            }
            //if(e.Brawler is ElPrimo elPrimo)
            //{
            //    GameObject coltObj = Instantiate(ColtPresenterPrefab, transform);
            //    _coltPresenter = coltObj.GetComponent<ColtPresenter>();
            //    _coltPresenter.Model = colt;
            //    _coltPresenter.transform.position = new Vector3(0, 0, 0);
            //    _coltPresenter.transform.rotation = Quaternion.identity;
            //    _coltPresenter.gameObject.SetActive(true);
            //    _coltPresenter.AddPlayerInput(PlayerInput);
            //    _coltPresenter.AddHBPresenter();
            //}
        }
    }
}
