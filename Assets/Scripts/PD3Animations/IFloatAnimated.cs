using UnityEngine;

namespace PD3Animations
{
    public interface IFloatAnimated
    {
        public void ValueChanged(float previousValue, float currentValue, float progress);

        public void AnimationEnded();
    }
}
