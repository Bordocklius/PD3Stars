using PD3Stars.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PD3Stars.Presenters
{
    public class HUDTextProviderPresenter
    {
        private IHUDProvider _model;
        public IHUDProvider Model
        {
            get { return _model; }
            set
            {
                if (_model == value)
                    return;

                if(_model != null)
                {
                    _model.PropertyChanged -= Model_OnPropertyChanged;
                    //_model.HealthChanged -= Model_OnHealthChanged;
                    //_model.PALoadingProgressChanged -= Model_OnPALoadingProgressChanged;
                }

                _model = value;
                _model.PropertyChanged += Model_OnPropertyChanged;
                //_model.HealthChanged += Model_OnHealthChanged;
                //_model.PALoadingProgressChanged += Model_OnPALoadingProgressChanged;
            }
        }

        public TextMeshProUGUI HealthText { get; private set; }
        public TextMeshProUGUI PaLoadingProgressText { get; private set; }

        public HUDTextProviderPresenter(IHUDProvider model, TextMeshProUGUI healthText, TextMeshProUGUI pAProgressText)
        {
            Model = model;
            HealthText = healthText;
            PaLoadingProgressText = pAProgressText;

            UpdateHealthText();
            UpdatePALoadingProgessText();
        }

        protected void Model_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(nameof(IHUDProvider.Health)))
                UpdateHealthText();

            if (e.PropertyName.Equals(nameof(IHUDProvider.PALoadTimer)))
                UpdatePALoadingProgessText();
        }

        protected virtual void Model_OnHealthChanged(object sender, EventArgs e)
        {
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            HealthText.text = $"Health: {Model.Health}";
        }

        protected virtual void Model_OnPALoadingProgressChanged(object sender, EventArgs e)
        {
            UpdatePALoadingProgessText();
        }

        private void UpdatePALoadingProgessText()
        {
            PaLoadingProgressText.text = $"PA: {Model.PALoadingProgress}";
        }
    }
}
