using System;
using UnityEngine;
using PD3Animations;

public class LinearMoveInSpiral : MonoBehaviour
{
    private Vector3 _finalLocalPosition;

    private void Awake()
    {
        _finalLocalPosition = transform.localPosition;
    }

    public void FloatChanged(object sender, ValueChangedEventArgs<float> e)
    {
        transform.localPosition = _finalLocalPosition * e.CurrentValue;
    }

    public void AnimationEnded(object sender, EventArgs e)
    {

    }
}
