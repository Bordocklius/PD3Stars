using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Animations
{
    public class GenericAnimationBuilder<T>
    {
        private GenericAnimation<T> _genericAnimation;

        public GenericAnimationBuilder()
        {
            _genericAnimation = new GenericAnimation<T>();
            _genericAnimation.InitFSM();
        }

        public GenericAnimation<T> Build()
        {
            return _genericAnimation;
        }

        public GenericAnimationBuilder<T> From(T from)
        {
            _genericAnimation.From = from;
            return this;
        }

        public GenericAnimationBuilder<T> To(T to)
        {
            _genericAnimation.To = to;
            return this;
        }

        public GenericAnimationBuilder<T> LerpT(Func<T, T, float, T> lerpT)
        {
            _genericAnimation.LerpT = lerpT;
            return this;
        }

        public GenericAnimationBuilder<T> EasingStrategy(IEasingStrategy easingStrategy)
        {
            _genericAnimation.EasingStrategy = easingStrategy;
            return this;
        }

        public GenericAnimationBuilder<T> EaseType(Ease ease)
        {
            _genericAnimation.Ease = ease;
            return this;
        }

        public GenericAnimationBuilder<T> DeltaTime(Func<float> deltatime)
        {
            _genericAnimation.DeltaTime = deltatime;
            return this;
        }
    }
}
