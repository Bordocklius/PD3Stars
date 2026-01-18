using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Animations
{
    /// <summary>
    /// Wrap Ease delegate into easing strategy
    /// </summary>
    public class EasingDelegateStrategy: IEasingStrategy
    {
        private Ease _ease;

        public EasingDelegateStrategy(Ease ease)
        {
            _ease = ease;
        }

        // use linear progress on ease
        public float Evaluate(float linearProgress)
        {
            //return MathF.Min(1, _ease(totalElapsed, 0, 1f, duration));
            return MathF.Min(1, _ease(linearProgress, 0, 1, 1));
        }
    }
}
