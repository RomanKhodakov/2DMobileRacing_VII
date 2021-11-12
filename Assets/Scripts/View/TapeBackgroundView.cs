using Tools;
using UnityEngine;

public class TapeBackgroundView : MonoBehaviour
{ //класс для подписи флот значения к методу Move
    [SerializeField] private Background[] _backgrounds;

    private IReadOnlySubscriptionProperty<float> _tapeBackgroundProperty; //некое флот значение, которое при изменении вызывает метод Move

    public void SetAndSubscribe(IReadOnlySubscriptionProperty<float> tapeBackgroundProperty)
    {
        _tapeBackgroundProperty = tapeBackgroundProperty;
        _tapeBackgroundProperty.SubscribeOnChange(Move);
    }

    private void Move(float value)
    {
        foreach (var background in _backgrounds)
            background.MoveBackground(-value);
    }
    
    protected void OnDestroy()
    {
        _tapeBackgroundProperty?.UnSubscriptionOnChange(Move);
    }
}

