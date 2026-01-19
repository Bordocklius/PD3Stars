using PD3Animations;
using PD3Stars.Models;
using PD3Stars.Models.ElPrimoModels;
using PD3Stars.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public class ElPrimoPresenter: BrawlerPresenter
    {
        [Space(10)]
        [Header("DashProperties")]
        [SerializeField]
        private InitialWeaponStats _dashStats;
        private GenericAnimation<Vector3> _dashAnimation;
        [SerializeField]
        private float _dashDistance = 10f;
        [SerializeField]
        private float _dashDuration = 0.2f; 
        [SerializeField]
        private AnimationCurve _dashCurve;

        protected override void ModelSetInitialisation(Brawler previousModel)
        {
            base.ModelSetInitialisation(previousModel);
            if(previousModel != null )
            {
                ElPrimo previousElPrimo = previousModel as ElPrimo;
                previousElPrimo.ElPrimoDashed -= Model_OnElPrimoDashed;
            }
            ElPrimo currentModel = Model as ElPrimo;
            if(currentModel != null)
            {
                currentModel.ElPrimoDashed += Model_OnElPrimoDashed;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _dashAnimation = new GenericAnimation<Vector3>();
            _dashAnimation.LerpT = Vector3.Lerp;
            _dashAnimation.Duration = _dashDuration;
            _dashAnimation.DeltaTime = () => Time.deltaTime;
            _dashAnimation.ValueChanged += DashAnimation_OnValueChanged;
            _dashAnimation.AnimationEnded += DashAnimation_OnAnimationEnded;
        }

        protected void Start() 
        {
            (Model as ElPrimo).DashDamage = _dashStats.Damage;
            
        }

        public override void OnPrimaryAttack()
        {
            Debug.Log("ElPrimo bonk");
            base.OnPrimaryAttack();
        }

        protected virtual void Model_OnElPrimoDashed(object sender, EventArgs e)
        {
            _dashAnimation.From = Transform.position;
            _dashAnimation.To = Transform.position + PAStrategy.AttackDirection.normalized * _dashDistance;
            StartCoroutine(_dashAnimation.StartAnimation());
        }

        protected virtual void DashAnimation_OnValueChanged(object sender, ValueChangedEventArgs<Vector3> e)
        {
            Transform.position = e.CurrentValue;
        }

        protected virtual void DashAnimation_OnAnimationEnded(object sender, EventArgs e)
        {
            (Model as ElPrimo).PAEnded();
        }

        // Triggers when dashing into a brawler
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damagableObj))
            {
                damagableObj.TakeDamage((Model as ElPrimo).DashDamage);
            }
        }

    }
}
