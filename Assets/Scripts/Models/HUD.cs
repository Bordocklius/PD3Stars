using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public class HUD: UnityModelBaseClass
    {
        private List<IHUDProvider> _hudProviders = new List<IHUDProvider>(3);

        public event EventHandler<HUDProviderPropertyChangedEventArgs> HUDProviderPropertyChanged;

        public HUD()
        {

        }

        public bool TryAddHUDProvider(IHUDProvider provider)
        {
            if(_hudProviders.Count >= 3) return false;

            _hudProviders.Add(provider);

            if (provider is INotifyPropertyChanged)
                provider.PropertyChanged += Provider_OnPropertyChanged;

            int index = _hudProviders.IndexOf(provider);
            HUDProviderPropertyChanged?.Invoke(this, new HUDProviderPropertyChangedEventArgs(provider, index, nameof(provider.Health)));
            HUDProviderPropertyChanged?.Invoke(this, new HUDProviderPropertyChangedEventArgs(provider, index, nameof(provider.PALoadTimer)));

            return true;
        }

        public void RemoveHUDProvider(IHUDProvider provider)
        {
            if (provider is INotifyPropertyChanged)
                provider.PropertyChanged -= Provider_OnPropertyChanged;

            _hudProviders.Remove(provider);
        }

        protected virtual void Provider_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IHUDProvider provider = (IHUDProvider)sender;
            
            int index = _hudProviders.IndexOf(provider);

            HUDProviderPropertyChanged?.Invoke(this, new HUDProviderPropertyChangedEventArgs(provider, index, e.PropertyName));
        }
    }

    public class HUDProviderPropertyChangedEventArgs: EventArgs
    {
        public IHUDProvider Provider { get; set; }
        public int ProviderIndex { get; set; }
        public string PropertyName { get; set; }

        public HUDProviderPropertyChangedEventArgs(IHUDProvider provider, int providerIndex, string propertyName)
        {
            Provider = provider;
            ProviderIndex = providerIndex;
            PropertyName = propertyName;
        }
    }
}
