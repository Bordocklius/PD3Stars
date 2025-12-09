using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PD3Stars.Models
{
    public abstract class ModelBaseClass: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
