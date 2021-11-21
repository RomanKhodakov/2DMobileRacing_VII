using JoostenProductions;
using Tools;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public sealed class FloatInputJoystick : BaseInputView
{
    private float _moveStep;
    public override void Initialization(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove,
        CarModel carModel)
    {
        base.Initialization(leftMove, rightMove, carModel);
        UpdateManager.SubscribeToUpdate(Move);
    }

    private void Move()
    {
        _moveStep = CarModel.Speed * Time.deltaTime * CrossPlatformInputManager.GetAxis("Horizontal");
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