using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PD3Animations
{
    public partial class GenericAnimation<T>
    {
        public event EventHandler<ValueChangedEventArgs<T>> ValueChanged;
        public event EventHandler AnimationReset;
        public event EventHandler AnimationEnded;

        public AnimationFSM FSM;

        public T From { get; set; }
        public T To { get; set; }
        public float Duration { get; set; }

        private float _totalElapsed;
        public float TotalElapsed
        {
            get { return _totalElapsed; }
            set
            {
                value = Mathf.Clamp(value, 0, Duration);
                if (TotalElapsed == value)
                    return;

                T previousValue = ProgressValue;
                _totalElapsed = value;
                ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>(previousValue, ProgressValue, Progress));

                if (TotalElapsed >= Duration)
                    AnimationEnded?.Invoke(this, EventArgs.Empty);

            }
        }

        public float Progress
        {
            get
            {
                float linearProgress = Mathf.Min(1, TotalElapsed / Duration);
                float easedProgress;

                if(Ease != null)
                    easedProgress = Ease(linearProgress, 0, 1, 1);
                else 
                    easedProgress = linearProgress;
                    return easedProgress;
            }
        }

        public Func<T, T, float, T> LerpT { get; set; }

        public Func<float> DeltaTime { get; set; }

        public Ease Ease { get; set; }

        public T ProgressValue
        {
            get => LerpT(From, To, Progress);
        }

        public GenericAnimation() { }

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

        public GenericAnimation(T from, T to, Func<T, T, float, T> lerpT, Ease ease, Func<float> deltaTime)
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

        public IEnumerator StartAnimation() 
        {
            if(DeltaTime == null)
            {
                Debug.Log("No deltatime function assigned");
            }
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