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
            Colt previousColtModel = previousModel as Colt;
            if (previousColtModel != null)
            {
                previousColtModel.ColtFired -= Model_OnColtFired;
            }
            (Model as Colt).ColtFired += Model_OnColtFired;
            (Model as Colt).MagSize = _magSize;
        }

        public override void OnPrimaryAttack(Vector3 attackDirection)
        {
            //Debug.Log("Click");
            base.OnPrimaryAttack(attackDirection);
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
