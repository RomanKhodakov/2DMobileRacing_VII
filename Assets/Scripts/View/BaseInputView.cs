using Tools;
using UnityEngine;

public abstract class BaseInputView : MonoBehaviour
{
    private SubscriptionProperty<float> _leftMove;
    private SubscriptionProperty<float> _rightMove;
    private float _speed;
    
    public virtual void Initialization(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        _speed = speed;
    }
    
    protected void OnLeftMove(float value)
    {
        _leftMove.Value = value;
    }

    protected void OnRightMove(float value)
    {
        _rightMove.Value = value;
    }
}

