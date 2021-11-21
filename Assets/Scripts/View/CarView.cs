using UnityEngine;

public sealed class CarView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _wheelsSpritesRenderers;
    [SerializeField] private SpriteRenderer _carFrameSpriteRenderer;

    public SpriteRenderer[] WheelsSpritesRenderers
    {
        get => _wheelsSpritesRenderers;
        set => _wheelsSpritesRenderers = value;
    }

    public SpriteRenderer CarFrameSpriteRenderer
    {
        get => _carFrameSpriteRenderer;
        set => _carFrameSpriteRenderer = value;
    }
}