using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomStartButton : Button
{
    private const float MoveDuration = 0.5f;
    private Vector3 _buttonRotation;

    private RectTransform _rectTransform;

    protected override void Awake()
    {
        base.Awake();

        _rectTransform = GetComponent<RectTransform>();
        _buttonRotation.Set(0, 0, 360);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        _rectTransform.DORotate(_buttonRotation, MoveDuration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        _rectTransform.DOKill();
        _rectTransform.DORotate(Vector3.zero, MoveDuration);
    }
}
