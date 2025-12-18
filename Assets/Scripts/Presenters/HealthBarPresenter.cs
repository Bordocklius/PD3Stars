using PD3Stars.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PD3Stars.Presenters
{
    public class HealthBarPresenter
    {
        private IHealthBar _model;
        public IHealthBar Model
        {
            get { return _model; }
            set
            {
                if (_model == value)
                    return;

                if (_model != null)
                {
                    _model.PropertyChanged -= Model_OnPropertyChanged;
                    //_model.HealthChanged -= Model_OnHealthChanged;
                }

                _model = value;
                _model.PropertyChanged += Model_OnPropertyChanged;
                //_model.HealthChanged += Model_OnHealthChanged;
            }
        }

        public Image FillImage;

        public HealthBarPresenter(IHealthBar model, Image fillImage)
        {
            Model = model;
            FillImage = fillImage;
            UpdateHealthBar(Model.HealthProgress);
        }

        protected void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IHealthBar.Health)))
                UpdateHealthBar();
        }

        protected virtual void Model_OnHealthChanged(object sender, EventArgs e)
        {
            UpdateHealthBar(Model.HealthProgress);
        }

        private void UpdateHealthBar()
        {
            FillImage.fillAmount = Mathf.Clamp01(Model.HealthProgress);
        }

        private void UpdateHealthBar(float progressValue)
        {
            FillImage.fillAmount = Mathf.Clamp01(progressValue);
        }
    }
}
