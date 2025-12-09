using PD3Stars.Models.ColtModels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PD3Stars.Presenters
{
    public class ColtPresenter : BrawlerPresenter<Colt>
    {
        [SerializeField]
        private GameObject _coltBulletPrefab;
        [SerializeField]
        private int _magSize;
        [SerializeField]
        private Transform _barrelPoint;
        [SerializeField]
        private PlayerInput _playerInput;

        protected void Awake()
        {
            //Model = new Colt();
            //Model.MagSize = _magSize;
        }

        protected override void ModelSetInitialisation(Colt previousModel)
        {
            base.ModelSetInitialisation(previousModel);
            if (previousModel != null)
            {
                previousModel.ColtFired -= Model_OnColtFired;
            }
            Model.ColtFired += Model_OnColtFired;
            Model.MagSize = _magSize;
        }

        protected override void OnPrimaryAttack(InputValue inputValue)
        {
            //Debug.Log("Click");
            base.OnPrimaryAttack(inputValue);
        }

        protected virtual void Model_OnColtFired(object sender, ColtFiredEventArgs e)
        {
            Debug.Log("Pjew");
            GameObject bullet = Instantiate(_coltBulletPrefab);
            bullet.transform.position = _barrelPoint.position;
            e.ColtBullet.SetBulletDirection(_barrelPoint.position, Model.AttackTarget);
            bullet.GetComponent<ColtBulletPresenter>().Model = e.ColtBullet;
        }

        public void AddPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }
    }
}
