using PD3Stars.Models;
using System;
using System.ComponentModel;
using UnityEngine;
using PD3Stars.Models.ColtModels;

namespace PD3Stars.Presenters
{
    public class ColtBulletPresenter : PresenterBaseClass<ColtBullet>
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private float _bulletSpeed;

        protected override void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if(e.PropertyName.Equals(nameof(ColtBullet.TTLTimer)))
            //    OnTTLTimerExpired();
        }

        protected override void ModelSetInitialisation(ColtBullet previousModel)
        {
            if(previousModel != null)
            {
                previousModel.TTLExpired -= Model_OnTTLTimerExpired;
            }
            Model.TTLExpired += Model_OnTTLTimerExpired;
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

        protected virtual void Model_OnTTLTimerExpired(object sender, EventArgs e)
        {
            Destroy(this.gameObject);
        }
    }
}
