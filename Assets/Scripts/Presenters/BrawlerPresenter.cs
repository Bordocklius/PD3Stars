using PD3Stars.Models;
using PD3Stars.Strategies.Movement;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PD3Stars.Presenters
{
    public abstract class BrawlerPresenter<TBrawler> : PresenterBaseClass<TBrawler> where TBrawler : Brawler
    {
        [Header("Movement")]
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private Transform _transform;
        [SerializeField]
        private float _rotationSpeed;
        [SerializeField]
        private InputSystem_Actions _inputActions;

        private Vector2 _movementInput;

        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private LayerMask _groundMask;


        [SerializeField]
        private Image HealthBar;
        private HealthBarPresenter _HBPresenter;


        private IMovementStrategy _movementStrategy;
        public IMovementStrategy MovementStrategy
        {
            get { return _movementStrategy; }
            set
            {
                if (_movementStrategy == value)
                    return;
                _movementStrategy = value;
            }
        }

        protected override void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Brawler.Health)))
                ShowHealth();
        }

        //protected virtual void ModelSetInitialisation(TBrawler previousModel)
        //{

        //}

        public void AddHBPresenter() => _HBPresenter = new HealthBarPresenter(Model, HealthBar);

        public void OnEnable()
        {
            if(_inputActions != null)
            {
                _inputActions.PlayerInput.Move.Enable();
            }
                _inputActions.PlayerInput.Enable();

            InputAction test = _inputActions.PlayerInput.Move;
        }

        protected override void Update()
        {
            base.Update();
            HandleMovement();
            HandleRotation();
        }

        private void OnMove(InputValue inputValue)
        {
            _movementInput = inputValue.Get<Vector2>();
        }

        private void HandleMovement()
        {
            if (_movementInput != null)
            {
                Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y) * _movementSpeed * Time.deltaTime;
                _transform.position += movement;
            }
        }

        private void HandleRotation()
        {
            // Look towards ground based on where mouse is positioned
            Vector3 targetPoint = GetMousePosition();

            // Direction from character to mouse hit point
            Vector3 direction = targetPoint - _transform.position;
            direction.y = 0f; // Prevent tilting up/down

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            }
        }

        protected virtual void OnPrimaryAttack(InputValue inputValue)
        {
            Model?.SetAttackTarget(GetMousePosition());
            Model?.PARequested();
        }

        private Vector3 GetMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint = new Vector3();
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _groundMask))
            {
                targetPoint = hitInfo.point;
            }
            return targetPoint;
        }

        private void ShowHealth()
        {
            Debug.Log($"BrawlerHealth: {Model.Health}");
        }
    }
}
