using JoostenProductions;
using Tools;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public sealed class FloatInputJoystick : BaseInputView
{
    private float _moveStep;
    public override void Initialization(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove,
        float speed)
    {
        base.Initialization(leftMove, rightMove, speed);
        UpdateManager.SubscribeToUpdate(Move);
    }

    private void Move()
    {
        _moveStep = Speed * Time.deltaTime * CrossPlatformInputManager.GetAxis("Horizontal");
        if (_moveStep > 0)
            OnRightMove(_moveStep);
        else if (_moveStep < 0)
            OnLeftMove(_moveStep);
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
    }
}