using PD3Stars.Models;
using PD3Stars.Models.ColtModels;
using PD3Stars.ScriptableObjects;
using System.ComponentModel;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public class ColtBulletPresenter : PresenterBaseClass<ColtBullet>, IDamageSource, IGetModel
    {
        public GameObject Source => this.gameObject;
        public float Damage => Model.Damage;

        [SerializeField]
        private InitialWeaponStats _initialBulletStats;

        [SerializeField]
        private Transform _transform;

        public Vector3 BulletDirection;

        private float _bulletSpeed;

        public UnityModelBaseClass GetModel() => Model;

        protected override void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ColtBullet.IsActive)))
                Model_OnDeactivated();
        }

        protected override void ModelSetInitialisation(ColtBullet previousModel)
        {
            if (previousModel != null)
            {
                previousModel.TeleportEvent -= Model_OnTeleport;
            }
            Model.TeleportEvent += Model_OnTeleport;
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
            _transform.position += BulletDirection * _bulletSpeed * Time.fixedDeltaTime;
            _transform.rotation = Quaternion.LookRotation(BulletDirection);
        }

        protected virtual void Model_OnDeactivated()
        {
            this.gameObject.SetActive(false);
        }

        protected virtual void Model_OnTeleport(object sender, TeleportEventArgs e)
        {
            // Convert system.numerics vector3 to unityengine vector 3 for position
            Vector3 newPos = new Vector3(e.NewPosition.X, _transform.position.y, e.NewPosition.Z);

            _transform.position = newPos;
        }
        //protected virtual void Model_OnTTLTimerExpired(object sender, EventArgs e)
        //{
        //    Destroy(this.gameObject);
        //}

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IDamageable>(out IDamageable damagableObj))
            {
                damagableObj.TakeDamage(Model.Damage, this);
                Model.IsActive = false;
            }
        }

    }
}
