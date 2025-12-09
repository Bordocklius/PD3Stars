using PD3Stars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PD3Stars.Presenters
{
    public class HealthBarPresenter
    {
        public IHealthBar Model { get; private set; }
        public Image FillImage;

        public HealthBarPresenter(IHealthBar model, Image fillImage)
        {
            Model = model;
            FillImage = fillImage;
            Model.HealthChanged += Model_OnHealthChanged;
            UpdateHealthBar(0f);
        }

        protected virtual void Model_OnHealthChanged(object sender, EventArgs e)
        {
            UpdateHealthBar(Model.HealthProgress);
        }

        private void UpdateHealthBar(float progressValue)
        {
            FillImage.fillAmount = Mathf.Clamp01(progressValue);
        }
    }
}
