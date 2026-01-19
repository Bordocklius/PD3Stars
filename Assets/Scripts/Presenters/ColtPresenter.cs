using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PD3Stars.Presenters
{
    public class ColtPresenter : BrawlerPresenter
    {
        // Explicitly cast Model as Colt to avoid exessive casting
        public new Colt Model => (Colt)base.Model;

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

            Model.ColtFired += Model_OnColtFired;
            Model.MagSize = _magSize;

            _bulletObjPool = new Dictionary<ColtBullet, GameObject>(Model.MagSize);
            foreach (ColtBullet bulletModel in Model.BulletPool)
            {
                AddBulletToPool(bulletModel);
            }        
        }

        public override void OnPrimaryAttack()
        {
            base.OnPrimaryAttack();

        }

        protected virtual void Model_OnColtFired(object sender, ColtFiredEventArgs e)
        {
            // add bullet to pool if it doesnt exist in pool
            if (!_bulletObjPool.TryGetValue(e.ColtBullet, out GameObject bullet))
            {
                bullet = AddBulletToPool(e.ColtBullet);
            }

            bullet.transform.position = _barrelPoint.position;
            ColtBulletPresenter bulletPresenter = bullet.GetComponent<ColtBulletPresenter>();
            bulletPresenter.Model = e.ColtBullet;
            Vector3 direction = (PAStrategy.AttackDirection - _barrelPoint.position).normalized;
            direction.y = 0;
            bulletPresenter.BulletDirection = direction;
            bullet.SetActive(true);
        }

        private GameObject AddBulletToPool(ColtBullet bulletModel)
        {
            GameObject bulletObj = Instantiate(_coltBulletPrefab);
            bulletObj.SetActive(false);
            bulletObj.GetComponent<ColtBulletPresenter>().Model = bulletModel;
            _bulletObjPool.Add(bulletModel, bulletObj);
            //_bulletObjPool[bulletModel] = bulletObj;
            return bulletObj;
        }

    }
}
