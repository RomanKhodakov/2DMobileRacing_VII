using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomStartButton : Button
{
    private readonly int _width = Screen.width;
    private readonly int _height = Screen.height;
    private const float MoveDuration = 0.5f;
    private const float MoveRate = 1.5f;

    private RectTransform _rectTransform;

    protected override void Awake()
    {
        base.Awake();

        _rectTransform = GetComponent<RectTransform>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (_rectTransform.position.x < _width / 2 && _rectTransform.position.y < _height / 2)
        {
            _rectTransform.DOMove(_rectTransform.position * MoveRate, MoveDuration).SetEase(Ease.InOutCirc);
        }
        else
        {
            _rectTransform.DOMove(_rectTransform.position * (1 / MoveRate), MoveDuration).SetEase(Ease.InOutCirc);
        }
    }
}
