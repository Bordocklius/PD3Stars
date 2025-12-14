using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using System.Collections.Generic;
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

        private Dictionary<ColtBullet, GameObject> _bulletObjPool;

        protected override void ModelSetInitialisation(Brawler previousModel)
        {
            base.ModelSetInitialisation(previousModel);
            if (previousModel != null)
            {
                Colt previousColtModel = previousModel as Colt;
                previousColtModel.ColtFired -= Model_OnColtFired;
            }
            Colt currentModel = (Model as Colt);

            currentModel.ColtFired += Model_OnColtFired;
            currentModel.MagSize = _magSize;

            _bulletObjPool = new Dictionary<ColtBullet, GameObject>(currentModel.MagSize);
            foreach(ColtBullet bulletModel in currentModel.BulletPool)
            {
                GameObject bulletObj = Instantiate(_coltBulletPrefab);
                bulletObj.SetActive(false);
                bulletObj.GetComponent<ColtBulletPresenter>().Model = bulletModel;
                _bulletObjPool[bulletModel] = bulletObj;
            }
        }

        public override void OnPrimaryAttack(Vector3 attackDirection)
        {
            //Debug.Log("Click");
            base.OnPrimaryAttack(attackDirection);
            Debug.Log("Pjew");
        }

        protected virtual void Model_OnColtFired(object sender, ColtFiredEventArgs e)
        {
            GameObject bullet = _bulletObjPool[e.ColtBullet];
            bullet.transform.position = _barrelPoint.position;
            e.ColtBullet.SetBulletDirection(_barrelPoint.position, Model.AttackTarget);
            bullet.GetComponent<ColtBulletPresenter>().Model = e.ColtBullet;
            bullet.SetActive(true);
        }

    }
}
