using PD3Stars.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace PD3Stars.Presenters
{
    public class HUDPresenter : MonoBehaviour
    {
        public UIDocument UIDocument;
        private Label lblHealth;

        private Brawler _brawler;
        public Brawler Brawler
        {
            get { return _brawler; }
            set
            {
                if (_brawler == value)
                    return;
                if (_brawler != null)
                {
                    _brawler.HealthChangedValues -= OnHealthChanged;
                }
                _brawler = value;
                _brawler.HealthChangedValues += OnHealthChanged;
            }
        }

        private void Awake()
        {
            lblHealth = UIDocument.rootVisualElement.Q<Label>("lblHealth");
        }

        public void OnHealthChanged(object sender, BrawlerHealthEventArgs e)
        {
            lblHealth.text = e.NewHealth.ToString();
        }

    }
}
