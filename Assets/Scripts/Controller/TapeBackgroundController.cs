using Tools;
using UnityEngine;

public class TapeBackgroundController : BaseController
{ 
    private readonly ResourcePath _backgroundViewPath = new ResourcePath {PathResource = "Prefabs/background"};
    private TapeBackgroundView _tapeBackgroundView;
    private readonly SubscriptionProperty<float> _tapeBackgroundProperty;
    private readonly IReadOnlySubscriptionProperty<float> _leftMove;
    private readonly IReadOnlySubscriptionProperty<float> _rightMove;
    
    public TapeBackgroundController(IReadOnlySubscriptionProperty<float> leftMove, 
        IReadOnlySubscriptionProperty<float> rightMove)
    {
        _tapeBackgroundView = LoadBackgroundView();
        _tapeBackgroundProperty = new SubscriptionProperty<float>();
        
        _leftMove = leftMove;
        _rightMove = rightMove;
        
        _tapeBackgroundView.SetAndSubscribe(_tapeBackgroundProperty); // к загруженной вью прикрепляем переменную, которая,
        // когда меняется, вызывает метод MoveBG у всех BG. Меняется она на 36 строке
        
        _leftMove.SubscribeOnChange(Move); // когда у одной из этих переменных меняется значение, срабатывает метод Move
        _rightMove.SubscribeOnChange(Move);
    }

    private TapeBackgroundView LoadBackgroundView()
    {
        var backgroundGO = Object.Instantiate(ResourceLoader.LoadPrefab(_backgroundViewPath));
        AddGameObjects(backgroundGO);
        
        return backgroundGO.GetComponent<TapeBackgroundView>();
    }

    private void Move(float value)
    {
        _tapeBackgroundProperty.Value = value; // Срабатывает при изменении left(right)Move.Value и вызывает метод MoveBG у всех BG
    }

    protected override void OnDispose()
    {
        _leftMove.UnSubscriptionOnChange(Move);
        _rightMove.UnSubscriptionOnChange(Move);
        
        base.OnDispose();
    }
}

