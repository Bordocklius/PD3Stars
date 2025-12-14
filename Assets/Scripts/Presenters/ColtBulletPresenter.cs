using PD3Stars.Models;
using System;
using System.ComponentModel;
using UnityEngine;
using PD3Stars.Models.ColtModels;
using PD3Stars.ScriptableObjects;

namespace PD3Stars.Presenters
{
    public class ColtBulletPresenter : PresenterBaseClass<ColtBullet>
    {
        [SerializeField]
        private InitialWeaponStats _initialBulletStats;

        [SerializeField]
        private Transform _transform;

        private float _bulletSpeed;

        protected override void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ColtBullet.IsActive)))
                Model_OnDeactivated();
        }

        protected override void ModelSetInitialisation(ColtBullet previousModel)
        {
            //if(previousModel != null)
            //{
            //    previousModel.TTLExpired -= Model_OnTTLTimerExpired;
            //}
            //Model.TTLExpired += Model_OnTTLTimerExpired;
        }

        private void Start()
        {
            if(_transform == null)
                _transform = transform;

            Model.Damage = _initialBulletStats.Damage;
            _bulletSpeed = _initialBulletStats.Speed;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            HandleMovement();
        }

        private void HandleMovement()
        {
            _transform.position += Model.BulletDirection * _bulletSpeed * Time.fixedDeltaTime;
            _transform.rotation = Quaternion.LookRotation(Model.BulletDirection);
        }

        protected virtual void Model_OnDeactivated()
        {
            this.gameObject.SetActive(false);
        }

        //protected virtual void Model_OnTTLTimerExpired(object sender, EventArgs e)
        //{
        //    Destroy(this.gameObject);
        //}

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IDamageable>(out IDamageable damagableObj))
            {
                damagableObj.TakeDamage(Model.Damage);
                Model.IsActive = false;
            }
        }

    }
}
