using System;
using System.ComponentModel;

namespace PD3Stars.Models
{
    public interface IHealthBar: INotifyPropertyChanged
    {
        public float Health { get;}
        public float HealthProgress { get; }
    }
}