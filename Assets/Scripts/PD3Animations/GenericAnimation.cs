using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PD3Animations
{
    /// <summary>
    /// Class to animate a generic type between 2 values over a duration using a Lerp function & possible easing
    /// </summary>
    /// <typeparam name="T">Type that will be animated</typeparam>
    public partial class GenericAnimation<T>
    {
        /// <summary>
        /// Event raised when the animation progressed
        /// </summary>
        public event EventHandler<ValueChangedEventArgs<T>> ValueChanged;

        /// <summary>
        /// Event raised when the animation is reset
        /// </summary>
        public event EventHandler AnimationReset;

        /// <summary>
        /// Event raised when the animation has ended
        /// </summary>
        public event EventHandler AnimationEnded;

        /// <summary>
        /// FSM used to keep track of animation stated
        /// </summary>
        public AnimationFSM FSM;

        /// <summary>
        /// Value from where is animated
        /// </summary>
        public T From { get; set; }
        /// <summary>
        /// Value to where is animated
        /// </summary>
        public T To { get; set; }

        private float _duration;
        /// <summary>
        /// Duration of the animation
        /// </summary>
        public float Duration
        {
            get { return _duration; }
            set 
            { 
                if(value <= 0f)
                {
                    Debug.LogError("Animation duration <= 0, setting 1");
                    _duration = 1f;
                }
                else
                    _duration = value; 
            }
        }


        private float _totalElapsed;
        /// <summary>
        /// Elapsed time of the animation
        /// </summary>
        public float TotalElapsed
        {
            get { return _totalElapsed; }
            set
            {
                value = Mathf.Clamp(value, 0, Duration);
                if (_totalElapsed == value)
                    return;

                T previousValue = ProgressValue;
                _totalElapsed = value;
                ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>(previousValue, ProgressValue, Progress));

                if (_totalElapsed >= Duration)
                    AnimationEnded?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Progress value of the animation used in Lerp function
        /// </summary>
        public float Progress
        {
            get
            {              
                float linearProgress = Mathf.Min(1, TotalElapsed / Duration);
                float easedProgress;

                if (EasingStrategy != null)
                    easedProgress = EasingStrategy.Evaluate(linearProgress);
                    //return EasingStrategy.Evaluate(TotalElapsed, Duration);
                else 
                    easedProgress = linearProgress;

                //if (Ease != null)
                //    easedProgress = Ease(linearProgress, 0, 1, 1);

                return easedProgress;
            }
        }

        /// <summary>
        /// Assigned Lerp function delegate for type T of the animation
        /// </summary>
        public Func<T, T, float, T> LerpT { get; set; }

        /// <summary>
        /// Assigned DeltaTime function to progress animation time
        /// </summary>
        public Func<float> DeltaTime { get; set; }

        /// <summary>
        /// Easing strategy used for the animation
        /// </summary>
        public IEasingStrategy EasingStrategy { get; set; }

        // Set sets easing strategy for backwards compatibility
        private Ease _ease;
        public Ease Ease 
        { 
            get { return _ease; }
            set
            {
                if(value == _ease) return;

                _ease = value;
                EasingStrategy = new EasingDelegateStrategy(value);
            } 
        }

        /// <summary>
        /// Progress value for the animation
        /// </summary>
        public T ProgressValue
        {
            get => LerpT(From, To, Progress);
        }

        public GenericAnimation() 
        {
            FSM = new AnimationFSM(this);
        }

        public GenericAnimation(T from, T to, Func<T, T, float, T> lerpT, Ease ease)
        {
            From = from;
            To = to;
            Duration = 2f;
            TotalElapsed = 0f;
            LerpT = lerpT;
            FSM = new AnimationFSM(this);
            Ease = ease;
        }

        public GenericAnimation(T from, T to, Func<T, T, float, T> lerpT, Func<float> deltaTime, Ease ease)
        {
            From = from;
            To = to;
            Duration = 2f;
            TotalElapsed = 0f;
            LerpT = lerpT;
            FSM = new AnimationFSM(this);
            Ease = ease;
            DeltaTime = deltaTime;
        }

        /// <summary>
        /// Constructs a new GenericAnimation between 2 given values, using the provided lerp, easing, and deltatime functions
        /// </summary>
        /// <param name="from">Value to animate from</param>
        /// <param name="to">Value to animate to</param>
        /// <param name="lerpT">Lerp function for the generic type</param>
        /// <param name="deltaTime">Time function used to advance animation time</param>
        /// <param name="easingStrategy">Easing strategy used for this animation</param>
        public GenericAnimation(T from, T to, Func<T, T, float, T> lerpT, Func<float> deltaTime, IEasingStrategy easingStrategy)
        {
            From = from;
            To = to;
            Duration = 2f;
            TotalElapsed = 0f;
            LerpT = lerpT;
            FSM = new AnimationFSM(this);
            DeltaTime = deltaTime;
            EasingStrategy = easingStrategy;
        }

        public void InitFSM()
        {
            FSM = new AnimationFSM(this);
        }

        public IEnumerator StartAnimation() 
        {
            if(DeltaTime == null)
            {
                Debug.LogError("No deltatime function assigned to animation");
                yield break;
            }
            if(FSM == null)
                InitFSM();
            if(FSM.CurrentState is AnimationEndedState)
                FSM.CurrentState.ResetAnimation();
            if(FSM.CurrentState is not AnimationCreatedState)
                yield break;
            FSM.CurrentState.StartAnimation();
            while(FSM.CurrentState is not AnimationEndedState)
            {
                FSM.FixedUpdate(DeltaTime());
                yield return null;
            }
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            FSM.FixedUpdate(fixedDeltaTime);
        }

        public void UpdateAnimation(float frameDuration)
        {
            TotalElapsed += frameDuration;
        }

        public void ResetAnimation()
        {
            TotalElapsed = 0f;
            AnimationReset?.Invoke(this, null);
        }

        public override string ToString()
        {
            return $"Animating from {From.ToString()} to {To.ToString()}";
        }
    }
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T PreviousValue { get; set; }
        public T CurrentValue { get; set; }
        public float Progress { get; set; }

        public ValueChangedEventArgs(T previousValue, T currentValue, float progress)
        {
            PreviousValue = previousValue;
            CurrentValue = currentValue;
            Progress = progress;
        }

    }

}