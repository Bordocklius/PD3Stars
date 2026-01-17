using Codice.CM.Common;
using PD3Stars.Models;
using PD3Stars.Strategies.Movement;
using PD3Stars.Strategies.PA;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PD3Stars.Presenters
{
    public abstract class BrawlerPresenter : PresenterBaseClass<Brawler>, IDamageable
    {
        [Header("Movement")]
        [SerializeField]
        private float _movementSpeed;
        [field: SerializeField]
        public Transform Transform { get; private set; }
        [field: SerializeField]
        public float RotationSpeed { get; private set; }

        [SerializeField]
        private InputSystem_Actions _inputActions;
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _paAction;

        private Vector2 _movementInput;

        [field: SerializeField]
        public Camera Camera { get; set; }
        [field: SerializeField]
        public LayerMask GroundMask { get; private set; }

        [SerializeField]
        private Image HealthBar;
        private HealthBarPresenter _HBPresenter;


        //private IMovementStrategy _movementStrategy;
        //public IMovementStrategy MovementStrategy
        //{
        //    get { return _movementStrategy; }
        //    set
        //    {
        //        if (_movementStrategy == value)
        //            return;
        //        _movementStrategy = value;
        //    }
        //}

        public IMovementStrategy MovementStrategy { get; set; }

        public IPAStrategy PAStrategy { get; set; }

        protected override void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName.Equals(nameof(Brawler.Health)))
            //    ShowHealth();
        }

        //protected virtual void ModelSetInitialisation(TBrawler previousModel)
        //{

        //}

        public void AddHBPresenter() => _HBPresenter = new HealthBarPresenter(Model, HealthBar);

        public void Awake()
        {
            if (Transform == null)
                Transform = this.transform;
            if (Camera == null)
                Camera = Camera.main;
        }

        protected override void Update()
        {
            base.Update();
            if(MovementStrategy != null)
            {
                MovementStrategy.Update(Time.deltaTime);            
                //HandleMovement();            
                //HandleRotation();
            }
            
            if(PAStrategy != null)
            {
                PAStrategy.Update(Time.deltaTime);
            }
        }

        private void HandleMovement()
        {
            if(MovementStrategy.MoveDirection != Vector2.zero)
            {
                Vector3 movement = new Vector3(MovementStrategy.MoveDirection.x, 0, MovementStrategy.MoveDirection.y) * _movementSpeed * Time.deltaTime;
                Transform.position += movement;
            }
        }

        public void MoveCharacter(Vector3 direction)
        {
            transform.position += direction * _movementSpeed * Time.deltaTime;
        }

        public void RotateCharacter(Vector3 direction)
        {
            direction = direction.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Transform.rotation = Quaternion.RotateTowards(Transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
        }

        private void HandleRotation()
        {
            if(MovementStrategy.RotationDirection != Vector3.zero)
            {
                Vector3 direction = MovementStrategy.RotationDirection.normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                Transform.rotation = Quaternion.RotateTowards(Transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
                //Transform.rotation = Quaternion.Slerp(Transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
            }

            //// Look towards ground based on where mouse is positioned
            //Vector3 targetPoint = GetMousePosition();

            //// Direction from character to mouse hit point
            //Vector3 direction = targetPoint - Transform.position;
            //direction.y = 0f; // Prevent tilting up/down

            //if (direction.sqrMagnitude > 0.001f)
            //{
            //    Quaternion targetRotation = Quaternion.LookRotation(direction);
            //    Transform.rotation = Quaternion.Slerp(Transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
            //}
        }

        public virtual void OnPrimaryAttack()
        {            
            Model?.PARequested();
        }

        public void TakeDamage(float damage)
        {            
            Model?.ReceiveDamage(damage);
        }
    }
}
