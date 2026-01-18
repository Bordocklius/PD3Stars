using UnityEngine;
using PD3Animations;

public class SpiralLogic : MonoBehaviour
{
    public AnimationCurve animationCurve;

    private bool _isSpinning = false;
    private Vector3 _finalEuler;

    //private GenericAnimation<float> _moveLinearAnimation =
    //    new GenericAnimation<float>(10, 1, Mathf.Lerp, EaseMethods.CircleEaseInOut);
    //private GenericAnimation<float> _moveLinearAnimation =
    //    new GenericAnimation<float>(10, 1, Mathf.Lerp, () => Time.deltaTime, new EasingDelegateStrategy(EaseMethods.CircleEaseInOut));

    private GenericAnimation<float> _moveLinearAnimation;

    //private GenericAnimation<float> _moveLinearAnimation = new GenericAnimationBuilder<float>().From(10).To(1).LerpT(Mathf.Lerp).DeltaTime(() => Time.deltaTime).EasingStrategy(new EasingDelegateStrategy(EaseMethods.CircleEaseInOut)).Build();

    //private GenericAnimation<float> _moveLinearAnimation = new GenericAnimationBuilder<float>().From(10).To(1).LerpT(Mathf.Lerp).EaseType(EaseMethods.CircleEaseInOut).Build();

    private void Awake()
    {
        _moveLinearAnimation = new GenericAnimation<float>(10, 1, Mathf.Lerp, () => Time.deltaTime, new AnimationCurveStrategy(animationCurve));


        _finalEuler = gameObject.transform.localEulerAngles;
        foreach(LinearMoveInSpiral sub in transform.GetComponentsInChildren<LinearMoveInSpiral>())
        {
            _moveLinearAnimation.ValueChanged += sub.FloatChanged;
        }
        _moveLinearAnimation.ValueChanged += ValueChanged;

        StartCoroutine(_moveLinearAnimation.StartAnimation());
    }
    private void Update()
    {
        if (!_isSpinning)
            return;
        _moveLinearAnimation.UpdateAnimation(Time.deltaTime);
    }

    private void AnimationEnded()
    {
        _isSpinning = false;
        _moveLinearAnimation.ResetAnimation();
    }

    public void ValueChanged(object sender, ValueChangedEventArgs<float> e)
    {
        gameObject.transform.localEulerAngles = _finalEuler + new Vector3(0, -720 * e.Progress, 0);
    }
    
}
