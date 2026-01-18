namespace PD3Animations
{
    public interface IEasingStrategy
    {
        // return value between 0 & 1 for a given progress of the animation
        public float Evaluate(float linearProgress);
    }

}
