using System;

namespace PD3Stars.Models
{
    public interface IHealthBar
    {
        public event EventHandler HealthChanged;
        public float HealthProgress { get; }
    }
}