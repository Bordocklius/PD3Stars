using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Animations
{
    /// <summary>
    /// Wrap animation curve into easing strategy
    /// </summary>
    public class AnimationCurveStrategy: IEasingStrategy
    {
        private AnimationCurve _animationCurve;

        public AnimationCurveStrategy(AnimationCurve animationCurve)
        {
            _animationCurve = animationCurve;
        }

        // Use linear progress on animationcurve evaluation
        public float Evaluate(float linearProgress)
        {
            //float t = MathF.Min(1, totalElapsed / duration);
            return Mathf.Min(1, _animationCurve.Evaluate(linearProgress));
        }
    }
}
