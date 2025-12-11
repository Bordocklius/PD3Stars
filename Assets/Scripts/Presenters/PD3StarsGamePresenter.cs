using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using PD3Stars.Models.ElPrimoModels;
using PD3Stars.Singleton;
using PD3Stars.Strategies.Movement;
using PD3Stars.Strategies.PA;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        [SerializeField]
        private List<GameObject> _prefabByName = new List<GameObject>();
        private List<GameObject> _brawlerObjects = new List<GameObject>();

        private void Awake()
        {
            inputActions = new InputSystem_Actions();
            Model = Singleton<PD3StarsGame>.Instance;
            Model.AddColt();
            //Model.AddElPrimo();
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
            GameObject prefab = _prefabByName.Where(x => x.name == e.Brawler.PrefabName).FirstOrDefault();
            if(prefab == null)
            {
                Debug.LogError($"No prefab found by name of {e.Brawler.PrefabName}");
                return;
            }

            GameObject brawlerObj = Instantiate(prefab, transform);
            InitializePresenter(e.Brawler, brawlerObj);

            //if(e.Brawler is Colt colt)
            //{
            //    GameObject coltObj = Instantiate(ColtPresenterPrefab, transform);
            //    _coltPresenter = coltObj.GetComponent<ColtPresenter>();
            //    _coltPresenter.Model = colt;
            //    _coltPresenter.transform.position = new Vector3(0, 0, 0);
            //    _coltPresenter.transform.rotation = Quaternion.identity;
            //    _coltPresenter.gameObject.SetActive(true);
            //    IMovementStrategy movementStrategy = new UserMovementStrategy(_coltPresenter.Model, _coltPresenter);
            //    //_coltPresenter.AddPlayerInput(PlayerInput);
            //    _coltPresenter.AddHBPresenter();
            //}
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

        private void InitializePresenter(Brawler brawler, GameObject gameobj)
        {
            _brawlerObjects.Add(gameobj);
            BrawlerPresenter brawlerPresenter = gameobj.GetComponent<BrawlerPresenter>();
            brawlerPresenter.Model = brawler;
            brawlerPresenter.transform.position = Vector3.zero;
            brawlerPresenter.transform.rotation = Quaternion.identity;
            brawlerPresenter.AddHBPresenter();

            if(_brawlerObjects.Count == 1)
            {

                Camera.main.GetComponent<CameraFollowScript>().SetTarget(brawlerPresenter.transform);
                brawlerPresenter.Camera = Camera.main;
                IMovementStrategy movementStrategy = new UserMovementStrategy(brawler, brawlerPresenter);
                (movementStrategy as UserMovementStrategy).SetInputActions(inputActions);
                brawlerPresenter.MovementStrategy = movementStrategy;

                IPAStrategy pAStrategy = new UserPAStrategy(brawler, brawlerPresenter);
                (pAStrategy as UserPAStrategy).SetInputActions(inputActions);
                brawlerPresenter.PAStrategy = pAStrategy;
            }
            else
            {
                IMovementStrategy movementStrategy = new RotateMovementStrategy(brawler, brawlerPresenter);
                brawlerPresenter.MovementStrategy = movementStrategy;
            }
        }
    }
}
