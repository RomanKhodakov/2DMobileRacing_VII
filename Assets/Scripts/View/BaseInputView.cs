using Tools;
using UnityEngine;

public abstract class BaseInputView : MonoBehaviour
{
    private SubscriptionProperty<float> _leftMove;
    private SubscriptionProperty<float> _rightMove;
    protected CarModel CarModel;
    
    public virtual void Initialization(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, CarModel carModel)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        CarModel = carModel;
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

