using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PD3Stars.Presenters
{
    public class ColtPresenter : BrawlerPresenter
    {
        [SerializeField]
        private GameObject _coltBulletPrefab;
        [SerializeField]
        private int _magSize;
        [SerializeField]
        private Transform _barrelPoint;

        protected void Awake()
        {
            //Model = new Colt();
            //Model.MagSize = _magSize;
        }

        protected override void ModelSetInitialisation(Brawler previousModel)
        {
            base.ModelSetInitialisation(previousModel);
            Colt coltModel = previousModel as Colt;
            if (previousModel != null)
            {
                coltModel.ColtFired -= Model_OnColtFired;
            }
            coltModel.ColtFired += Model_OnColtFired;
            coltModel.MagSize = _magSize;
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

    }
}
