namespace PD3Animations
{
    public interface IEasingStrategy
    {
        // return value between 0 & 1 for a given duration
        public float Evaluate(float linearProgress);
    }

}
