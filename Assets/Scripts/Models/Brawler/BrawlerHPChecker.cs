using System.ComponentModel;

namespace PD3Stars.Models
{
    public partial class Brawler
    {
        public class BrawlerHPChecker
        {
            private Brawler _context;

            private float PreviousHealth;

            protected virtual void Context_OnPropertChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(nameof(_context.Health)))
                {
                    CheckBrawlerHP();
                }
            }

            public BrawlerHPChecker(Brawler context)
            {
                _context = context;                
                _context.PropertyChanged += Context_OnPropertChanged;
            }

            private void CheckBrawlerHP()
            {
                // Check if brawler is dead
                if (_context.Health <= 0)
                {
                    _context.OnHealthDepleted();
                }
                // Check if brawler has taken damage
                else if (_context.Health < PreviousHealth)
                {                    
                    _context.OnHealthTookDamage();
                }

                PreviousHealth = _context.Health;
            }
        }
    }
    
}
