using Tools;
using UnityEngine;

public class InputGameController : BaseController
{
    private readonly ResourcePath _inputPath = new ResourcePath {PathResource = "Prefabs/MobileSingleStickControl"};
    private BaseInputView _inputView;
    public InputGameController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, CarModel carModel)
    {
        _inputView = LoadView();
        _inputView.Initialization(leftMove, rightMove, carModel);
    }

    private BaseInputView LoadView()
    {
        var inputGO = Object.Instantiate(ResourceLoader.LoadPrefab(_inputPath));
        AddGameObjects(inputGO);
        
        return inputGO.GetComponent<BaseInputView>();
    }
}

