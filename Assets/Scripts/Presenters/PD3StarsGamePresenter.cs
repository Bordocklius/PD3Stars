using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using PD3Stars.Models.ElPrimoModels;
using PD3Stars.Network;
using PD3Stars.ScriptableObjects;
using PD3Stars.Singleton;
using PD3Stars.Strategies.Movement;
using PD3Stars.Strategies.PA;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PD3Stars.Presenters
{
    public class PD3StarsGamePresenter: PresenterBaseClass<PD3StarsGame>
    {
        [SerializeField]
        private GameObject _hudTextPrefab;
        [SerializeField]
        private RectTransform _hudPrefabContainer;
        
        private HUDTextProviderPresenter[] _hudTextPresenters = new HUDTextProviderPresenter[3];

        [SerializeField]
        private TextMeshProUGUI[] _hpTexts = new TextMeshProUGUI[3];
        [SerializeField]
        private TextMeshProUGUI[] _paTexts = new TextMeshProUGUI[3];

        public InputReaderSO InputReader;

        [SerializeField]
        private List<GameObject> _prefabByName = new List<GameObject>();
        private List<GameObject> _brawlerObjects = new List<GameObject>();

        private BrawlerPresenter _playerCharachter;

        private void Awake()
        {
            Model = Singleton<PD3StarsGame>.Instance;
            //Model.AddColt();
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
        }

        private void InitializePresenter(Brawler brawler, GameObject gameobj)
        {
            _brawlerObjects.Add(gameobj);
            BrawlerPresenter brawlerPresenter = gameobj.GetComponent<BrawlerPresenter>();
            brawlerPresenter.Model = brawler;
            brawlerPresenter.transform.position = Vector3.zero;
            brawlerPresenter.transform.rotation = Quaternion.identity;
            brawlerPresenter.AddHBPresenter();

            int count = _brawlerObjects.Count - 1;
            HUDTextProviderPresenter hudPresenter = new HUDTextProviderPresenter(brawler, _hpTexts[count], _paTexts[count]);

            //GameObject newHUD = Instantiate(_hudTextPrefab, _hudPrefabContainer);
            //LayoutRebuilder.ForceRebuildLayoutImmediate(_hudPrefabContainer);
            //HUDTextProviderPresenter hudPresenter = new HUDTextProviderPresenter(brawler,)

            IMovementStrategy movementStrategy;
            IPAStrategy paStrategy;

            if(_brawlerObjects.Count == 1)
            {

                Camera.main.GetComponent<CameraFollowScript>().SetTarget(brawlerPresenter.transform);
                brawlerPresenter.Camera = Camera.main;
                movementStrategy = new UserMovementStrategy(brawler, brawlerPresenter);
                (movementStrategy as UserMovementStrategy).SetInputActions(InputReader);

                paStrategy = new UserPAStrategy(brawler, brawlerPresenter);
                (paStrategy as UserPAStrategy).SetInputActions(InputReader);

                _playerCharachter = brawlerPresenter;
            }
            else if(_brawlerObjects.Count == 2)
            {
                movementStrategy = new FollowTheTankStrategy(brawler, brawlerPresenter, _playerCharachter.transform);

                paStrategy = new PA_ASAPStrategy(brawler, brawlerPresenter);
            }
            else
            {
                movementStrategy = new RotateMovementStrategy(brawler, brawlerPresenter);

                paStrategy = new PA_ASAPStrategy(brawler, brawlerPresenter);
            }

            brawlerPresenter.MovementStrategy = movementStrategy;
            brawlerPresenter.PAStrategy = paStrategy;
        }
    }
}
